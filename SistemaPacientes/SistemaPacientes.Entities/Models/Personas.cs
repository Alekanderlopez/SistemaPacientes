namespace SistemaPacientes.Entities.Models;

// Tabla 5
public class Paciente : BaseEntity
{
    public string Nombres { get; set; } = "";
    public string Apellidos { get; set; } = "";
    public string Cedula { get; set; } = "";
    public DateTime FechaNacimiento { get; set; }
    public string Sexo { get; set; } = "M";
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }

    public int? AseguradoraId { get; set; }
    public Aseguradora? Aseguradora { get; set; }
}

// Tabla 6
public class Medico : BaseEntity
{
    public string Nombres { get; set; } = "";
    public string Apellidos { get; set; } = "";
    public string Exequatur { get; set; } = "";
    public string? Telefono { get; set; }
    public string? Email { get; set; }

    public int EspecialidadId { get; set; }
    public Especialidad Especialidad { get; set; } = null!;
}
