using Microsoft.EntityFrameworkCore;
using SistemaPacientes.BLL.Exceptions;
using SistemaPacientes.BLL.Interfaces;
using SistemaPacientes.DAL.Repositories;
using SistemaPacientes.Entities.DTOs;
using SistemaPacientes.Entities.Models;

namespace SistemaPacientes.BLL.Services;

public class AseguradoraService(IUnitOfWork uow) : BaseService<Aseguradora, AseguradoraDto>(uow), IAseguradoraService
{
    protected override string Etiqueta(Aseguradora e) => e.Nombre;
}

public class EspecialidadService(IUnitOfWork uow) : BaseService<Especialidad, EspecialidadDto>(uow), IEspecialidadService
{
    protected override string Etiqueta(Especialidad e) => e.Nombre;
}

public class SalaService(IUnitOfWork uow) : BaseService<Sala, SalaDto>(uow), ISalaService
{
    protected override string Etiqueta(Sala e) => $"{e.Nombre} (piso {e.Piso})";
}

public class MedicamentoService(IUnitOfWork uow) : BaseService<Medicamento, MedicamentoDto>(uow), IMedicamentoService
{
    protected override string Etiqueta(Medicamento e) => e.Nombre;
}

public class PacienteService(IUnitOfWork uow) : BaseService<Paciente, PacienteDto>(uow), IPacienteService
{
    protected override IQueryable<Paciente> Incluir(IQueryable<Paciente> q) => q.Include(p => p.Aseguradora);

    protected override void Enriquecer(Paciente e, PacienteDto d) => d.AseguradoraNombre = e.Aseguradora?.Nombre;

    protected override string Etiqueta(Paciente e) => $"{e.Nombres} {e.Apellidos}";

    protected override async Task ValidarAsync(PacienteDto dto)
    {
        var duplicada = await Repo.Query().AnyAsync(p => p.Cedula == dto.Cedula && p.Id != dto.Id);
        if (duplicada) throw new BusinessException("Ya existe un paciente con esa cédula.");
    }
}

public class MedicoService(IUnitOfWork uow) : BaseService<Medico, MedicoDto>(uow), IMedicoService
{
    protected override IQueryable<Medico> Incluir(IQueryable<Medico> q) => q.Include(m => m.Especialidad);

    protected override void Enriquecer(Medico e, MedicoDto d) => d.EspecialidadNombre = e.Especialidad?.Nombre;

    protected override string Etiqueta(Medico e) => $"Dr(a). {e.Nombres} {e.Apellidos}";

    protected override async Task ValidarAsync(MedicoDto dto)
    {
        var duplicado = await Repo.Query().AnyAsync(m => m.Exequatur == dto.Exequatur && m.Id != dto.Id);
        if (duplicado) throw new BusinessException("Ya existe un médico con ese exequátur.");
    }
}

public class CitaService(IUnitOfWork uow) : BaseService<Cita, CitaDto>(uow), ICitaService
{
    protected override IQueryable<Cita> Incluir(IQueryable<Cita> q) =>
        q.Include(c => c.Paciente).Include(c => c.Medico).Include(c => c.Sala);

    protected override void Enriquecer(Cita e, CitaDto d)
    {
        d.PacienteNombre = e.Paciente is null ? null : $"{e.Paciente.Nombres} {e.Paciente.Apellidos}";
        d.MedicoNombre = e.Medico is null ? null : $"Dr(a). {e.Medico.Nombres} {e.Medico.Apellidos}";
        d.SalaNombre = e.Sala?.Nombre;
    }

    protected override string Etiqueta(Cita e) =>
        $"{e.Paciente?.Nombres} {e.Paciente?.Apellidos} - {e.FechaHora:dd/MM/yyyy HH:mm}";

    protected override async Task ValidarAsync(CitaDto dto)
    {
        if (dto.Id == 0 && dto.FechaHora < DateTime.Now)
            throw new BusinessException("No se puede crear una cita en el pasado.");

        if (dto.Estado != "Cancelada")
        {
            var ocupado = await Repo.Query().AnyAsync(c =>
                c.MedicoId == dto.MedicoId && c.FechaHora == dto.FechaHora &&
                c.Estado != "Cancelada" && c.Id != dto.Id);
            if (ocupado) throw new BusinessException("El médico ya tiene una cita a esa fecha y hora.");
        }
    }
}

public class ConsultaService(IUnitOfWork uow) : BaseService<Consulta, ConsultaDto>(uow), IConsultaService
{
    protected override IQueryable<Consulta> Incluir(IQueryable<Consulta> q) =>
        q.Include(c => c.Cita).ThenInclude(ci => ci.Paciente);

    private static string Texto(Consulta e) =>
        $"{e.Cita?.Paciente?.Nombres} {e.Cita?.Paciente?.Apellidos} - {e.Cita?.FechaHora:dd/MM/yyyy}";

    protected override void Enriquecer(Consulta e, ConsultaDto d) => d.CitaTexto = Texto(e);

    protected override string Etiqueta(Consulta e) => $"Consulta #{e.Id} - {Texto(e)}";
}

public class DiagnosticoService(IUnitOfWork uow) : BaseService<Diagnostico, DiagnosticoDto>(uow), IDiagnosticoService
{
    protected override void Enriquecer(Diagnostico e, DiagnosticoDto d) => d.ConsultaTexto = $"Consulta #{e.ConsultaId}";

    protected override string Etiqueta(Diagnostico e) => e.Descripcion;
}

public class RecetaService(IUnitOfWork uow) : BaseService<Receta, RecetaDto>(uow), IRecetaService
{
    protected override IQueryable<Receta> Incluir(IQueryable<Receta> q) => q.Include(r => r.Medicamento);

    protected override void Enriquecer(Receta e, RecetaDto d) => d.MedicamentoNombre = e.Medicamento?.Nombre;
}
