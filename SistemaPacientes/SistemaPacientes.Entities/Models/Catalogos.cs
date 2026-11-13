namespace SistemaPacientes.Entities.Models;

// Tabla 1
public class Aseguradora : BaseEntity
{
    public string Nombre { get; set; } = "";
    public string? Telefono { get; set; }
}

// Tabla 2
public class Especialidad : BaseEntity
{
    public string Nombre { get; set; } = "";
    public string? Descripcion { get; set; }
}

// Tabla 3
public class Sala : BaseEntity
{
    public string Nombre { get; set; } = "";
    public int Piso { get; set; }
}

// Tabla 4
public class Medicamento : BaseEntity
{
    public string Nombre { get; set; } = "";
    public string? Laboratorio { get; set; }
    public string? Presentacion { get; set; }
}
