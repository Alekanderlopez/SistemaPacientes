namespace SistemaPacientes.BLL.Exceptions;

/// <summary>El registro solicitado no existe (HTTP 404).</summary>
public class NotFoundException(string mensaje) : Exception(mensaje);

/// <summary>Se violó una regla de negocio (HTTP 400).</summary>
public class BusinessException(string mensaje) : Exception(mensaje);
