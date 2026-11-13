namespace SistemaPacientes.Entities.DTOs;

/// <summary>Marca una propiedad FK: el formulario muestra un desplegable cargado desde api/{Endpoint}/lookup.</summary>
[AttributeUsage(AttributeTargets.Property)]
public class LookupAttribute : Attribute
{
    public string Endpoint { get; }
    public LookupAttribute(string endpoint) => Endpoint = endpoint;
}

/// <summary>Se muestra en la tabla pero no en el formulario.</summary>
[AttributeUsage(AttributeTargets.Property)]
public class SoloLecturaAttribute : Attribute { }

/// <summary>El formulario muestra un desplegable con estos valores fijos.</summary>
[AttributeUsage(AttributeTargets.Property)]
public class OpcionesAttribute : Attribute
{
    public string[] Valores { get; }
    public OpcionesAttribute(params string[] valores) => Valores = valores;
}
