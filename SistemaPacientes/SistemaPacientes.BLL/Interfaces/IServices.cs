using SistemaPacientes.Entities.DTOs;

namespace SistemaPacientes.BLL.Interfaces;

public interface IBaseService<TDto> where TDto : BaseDto
{
    Task<List<TDto>> ObtenerTodosAsync();
    Task<TDto?> ObtenerPorIdAsync(int id);
    Task<TDto> CrearAsync(TDto dto);
    Task ActualizarAsync(int id, TDto dto);
    Task EliminarAsync(int id);
    Task<List<LookupDto>> ObtenerLookupAsync();
}

public interface IAseguradoraService : IBaseService<AseguradoraDto> { }
public interface IEspecialidadService : IBaseService<EspecialidadDto> { }
public interface ISalaService : IBaseService<SalaDto> { }
public interface IMedicamentoService : IBaseService<MedicamentoDto> { }
public interface IPacienteService : IBaseService<PacienteDto> { }
public interface IMedicoService : IBaseService<MedicoDto> { }
public interface ICitaService : IBaseService<CitaDto> { }
public interface IConsultaService : IBaseService<ConsultaDto> { }
public interface IDiagnosticoService : IBaseService<DiagnosticoDto> { }
public interface IRecetaService : IBaseService<RecetaDto> { }
