using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.BLL.Interfaces;
using SistemaPacientes.Entities.DTOs;

namespace SistemaPacientes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class CrudController<TDto>(IBaseService<TDto> servicio) : ControllerBase
    where TDto : BaseDto
{
    [HttpGet]
    public async Task<ActionResult<List<TDto>>> Listar() => Ok(await servicio.ObtenerTodosAsync());

    [HttpGet("lookup")]
    public async Task<ActionResult<List<LookupDto>>> Lookup() => Ok(await servicio.ObtenerLookupAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TDto>> ObtenerPorId(int id)
    {
        var dto = await servicio.ObtenerPorIdAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<TDto>> Crear(TDto dto)
    {
        var creado = await servicio.CrearAsync(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, TDto dto)
    {
        await servicio.ActualizarAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await servicio.EliminarAsync(id);
        return NoContent();
    }
}
