using System.ComponentModel.DataAnnotations;

namespace SistemaPacientes.Entities.DTOs;

public abstract class BaseDto
{
    public int Id { get; set; }
}

public class LookupDto
{
    public int Id { get; set; }
    public string Texto { get; set; } = "";
}

public class AseguradoraDto : BaseDto
{
    [Required(ErrorMessage = "El nombre es obligatorio"), StringLength(100)]
    public string Nombre { get; set; } = "";

    [Phone(ErrorMessage = "Teléfono inválido"), StringLength(20)]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }
}

public class EspecialidadDto : BaseDto
{
    [Required(ErrorMessage = "El nombre es obligatorio"), StringLength(100)]
    public string Nombre { get; set; } = "";

    [StringLength(200)]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }
}

public class SalaDto : BaseDto
{
    [Required(ErrorMessage = "El nombre es obligatorio"), StringLength(100)]
    public string Nombre { get; set; } = "";

    [Range(0, 50, ErrorMessage = "El piso debe estar entre 0 y 50")]
    public int Piso { get; set; }
}

public class MedicamentoDto : BaseDto
{
    [Required(ErrorMessage = "El nombre es obligatorio"), StringLength(100)]
    public string Nombre { get; set; } = "";

    [StringLength(100)]
    public string? Laboratorio { get; set; }

    [StringLength(100)]
    [Display(Name = "Presentación")]
    public string? Presentacion { get; set; }
}

public class PacienteDto : BaseDto
{
    [Required(ErrorMessage = "Los nombres son obligatorios"), StringLength(80)]
    public string Nombres { get; set; } = "";

    [Required(ErrorMessage = "Los apellidos son obligatorios"), StringLength(80)]
    public string Apellidos { get; set; } = "";

    [Required(ErrorMessage = "La cédula es obligatoria"), StringLength(15)]
    [Display(Name = "Cédula")]
    public string Cedula { get; set; } = "";

    [Display(Name = "Fecha de nacimiento"), DataType(DataType.Date)]
    [Range(typeof(DateTime), "1900-01-01", "2100-01-01", ErrorMessage = "Fecha de nacimiento inválida")]
    public DateTime FechaNacimiento { get; set; } = DateTime.Today;

    [Opciones("M", "F")]
    public string Sexo { get; set; } = "M";

    [Phone(ErrorMessage = "Teléfono inválido"), StringLength(20)]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "Correo inválido"), StringLength(100)]
    public string? Email { get; set; }

    [StringLength(200)]
    [Display(Name = "Dirección")]
    public string? Direccion { get; set; }

    [Lookup("aseguradoras"), Display(Name = "Aseguradora")]
    public int? AseguradoraId { get; set; }

    [SoloLectura, Display(Name = "Aseguradora")]
    public string? AseguradoraNombre { get; set; }
}

public class MedicoDto : BaseDto
{
    [Required(ErrorMessage = "Los nombres son obligatorios"), StringLength(80)]
    public string Nombres { get; set; } = "";

    [Required(ErrorMessage = "Los apellidos son obligatorios"), StringLength(80)]
    public string Apellidos { get; set; } = "";

    [Required(ErrorMessage = "El exequátur es obligatorio"), StringLength(30)]
    [Display(Name = "Exequátur")]
    public string Exequatur { get; set; } = "";

    [Phone(ErrorMessage = "Teléfono inválido"), StringLength(20)]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "Correo inválido"), StringLength(100)]
    public string? Email { get; set; }

    [Lookup("especialidades"), Display(Name = "Especialidad")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione una especialidad")]
    public int EspecialidadId { get; set; }

    [SoloLectura, Display(Name = "Especialidad")]
    public string? EspecialidadNombre { get; set; }
}

public class CitaDto : BaseDto
{
    [Lookup("pacientes"), Display(Name = "Paciente")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione un paciente")]
    public int PacienteId { get; set; }

    [SoloLectura, Display(Name = "Paciente")]
    public string? PacienteNombre { get; set; }

    [Lookup("medicos"), Display(Name = "Médico")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione un médico")]
    public int MedicoId { get; set; }

    [SoloLectura, Display(Name = "Médico")]
    public string? MedicoNombre { get; set; }

    [Lookup("salas"), Display(Name = "Sala")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione una sala")]
    public int SalaId { get; set; }

    [SoloLectura, Display(Name = "Sala")]
    public string? SalaNombre { get; set; }

    [Display(Name = "Fecha y hora"), DataType(DataType.DateTime)]
    public DateTime FechaHora { get; set; } = DateTime.Today.AddDays(1).AddHours(9);

    [Required(ErrorMessage = "El motivo es obligatorio"), StringLength(200)]
    public string Motivo { get; set; } = "";

    [Opciones("Pendiente", "Confirmada", "Completada", "Cancelada")]
    public string Estado { get; set; } = "Pendiente";
}

public class ConsultaDto : BaseDto
{
    [Lookup("citas"), Display(Name = "Cita")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione una cita")]
    public int CitaId { get; set; }

    [SoloLectura, Display(Name = "Cita")]
    public string? CitaTexto { get; set; }

    [Required(ErrorMessage = "Los síntomas son obligatorios"), StringLength(500)]
    [Display(Name = "Síntomas")]
    public string Sintomas { get; set; } = "";

    [StringLength(500)]
    public string? Observaciones { get; set; }
}

public class DiagnosticoDto : BaseDto
{
    [Lookup("consultas"), Display(Name = "Consulta")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione una consulta")]
    public int ConsultaId { get; set; }

    [SoloLectura, Display(Name = "Consulta")]
    public string? ConsultaTexto { get; set; }

    [StringLength(20)]
    [Display(Name = "Código CIE-10")]
    public string? Codigo { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria"), StringLength(300)]
    [Display(Name = "Descripción")]
    public string Descripcion { get; set; } = "";
}

public class RecetaDto : BaseDto
{
    [Lookup("consultas"), Display(Name = "Consulta")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione una consulta")]
    public int ConsultaId { get; set; }

    [Lookup("medicamentos"), Display(Name = "Medicamento")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione un medicamento")]
    public int MedicamentoId { get; set; }

    [SoloLectura, Display(Name = "Medicamento")]
    public string? MedicamentoNombre { get; set; }

    [Required(ErrorMessage = "La dosis es obligatoria"), StringLength(100)]
    public string Dosis { get; set; } = "";

    [Required(ErrorMessage = "La frecuencia es obligatoria"), StringLength(100)]
    public string Frecuencia { get; set; } = "";

    [Range(1, 365, ErrorMessage = "La duración debe estar entre 1 y 365 días")]
    [Display(Name = "Duración (días)")]
    public int DuracionDias { get; set; } = 7;
}
