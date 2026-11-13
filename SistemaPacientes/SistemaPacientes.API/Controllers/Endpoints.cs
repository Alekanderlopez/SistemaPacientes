using SistemaPacientes.BLL.Interfaces;
using SistemaPacientes.Entities.DTOs;

namespace SistemaPacientes.API.Controllers;

// Cada controlador hereda el CRUD completo. La ruta sale del nombre: api/pacientes, api/medicos, ...
public class AseguradorasController(IAseguradoraService s) : CrudController<AseguradoraDto>(s);
public class EspecialidadesController(IEspecialidadService s) : CrudController<EspecialidadDto>(s);
public class SalasController(ISalaService s) : CrudController<SalaDto>(s);
public class MedicamentosController(IMedicamentoService s) : CrudController<MedicamentoDto>(s);
public class PacientesController(IPacienteService s) : CrudController<PacienteDto>(s);
public class MedicosController(IMedicoService s) : CrudController<MedicoDto>(s);
public class CitasController(ICitaService s) : CrudController<CitaDto>(s);
public class ConsultasController(IConsultaService s) : CrudController<ConsultaDto>(s);
public class DiagnosticosController(IDiagnosticoService s) : CrudController<DiagnosticoDto>(s);
public class RecetasController(IRecetaService s) : CrudController<RecetaDto>(s);
