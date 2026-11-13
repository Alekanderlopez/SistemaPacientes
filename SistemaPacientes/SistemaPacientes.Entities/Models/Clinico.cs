namespace SistemaPacientes.Entities.Models;

// Tabla 7
public class Cita : BaseEntity
{
    public int PacienteId { get; set; }
    public Paciente Paciente { get; set; } = null!;

    public int MedicoId { get; set; }
    public Medico Medico { get; set; } = null!;

    public int SalaId { get; set; }
    public Sala Sala { get; set; } = null!;

    public DateTime FechaHora { get; set; }
    public string Motivo { get; set; } = "";
    public string Estado { get; set; } = "Pendiente";
}

// Tabla 8
public class Consulta : BaseEntity
{
    public int CitaId { get; set; }
    public Cita Cita { get; set; } = null!;

    public string Sintomas { get; set; } = "";
    public string? Observaciones { get; set; }
}

// Tabla 9
public class Diagnostico : BaseEntity
{
    public int ConsultaId { get; set; }
    public Consulta Consulta { get; set; } = null!;

    public string? Codigo { get; set; }          // CIE-10
    public string Descripcion { get; set; } = "";
}

// Tabla 10
public class Receta : BaseEntity
{
    public int ConsultaId { get; set; }
    public Consulta Consulta { get; set; } = null!;

    public int MedicamentoId { get; set; }
    public Medicamento Medicamento { get; set; } = null!;

    public string Dosis { get; set; } = "";
    public string Frecuencia { get; set; } = "";
    public int DuracionDias { get; set; }
}
