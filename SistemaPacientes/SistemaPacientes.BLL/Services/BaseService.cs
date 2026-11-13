using Microsoft.EntityFrameworkCore;
using SistemaPacientes.BLL.Exceptions;
using SistemaPacientes.BLL.Interfaces;
using SistemaPacientes.BLL.Mapping;
using SistemaPacientes.DAL.Repositories;
using SistemaPacientes.Entities.DTOs;
using SistemaPacientes.Entities.Models;

namespace SistemaPacientes.BLL.Services;

/// <summary>CRUD genérico. Cada servicio concreto solo sobrescribe lo que necesita.</summary>
public abstract class BaseService<TEntity, TDto>(IUnitOfWork uow) : IBaseService<TDto>
    where TEntity : BaseEntity, new()
    where TDto : BaseDto, new()
{
    protected IUnitOfWork Uow { get; } = uow;
    protected IRepository<TEntity> Repo => Uow.Repository<TEntity>();

    // ---- puntos de extensión ----
    protected virtual IQueryable<TEntity> Incluir(IQueryable<TEntity> q) => q;
    protected virtual void Enriquecer(TEntity entidad, TDto dto) { }
    protected virtual string Etiqueta(TEntity entidad) => $"#{entidad.Id}";
    protected virtual Task ValidarAsync(TDto dto) => Task.CompletedTask;

    private TDto Convertir(TEntity entidad)
    {
        var dto = Mapper.Map<TEntity, TDto>(entidad);
        Enriquecer(entidad, dto);
        return dto;
    }

    public async Task<List<TDto>> ObtenerTodosAsync()
    {
        var lista = await Incluir(Repo.Query()).AsNoTracking().OrderByDescending(e => e.Id).ToListAsync();
        return lista.Select(Convertir).ToList();
    }

    public async Task<TDto?> ObtenerPorIdAsync(int id)
    {
        var entidad = await Incluir(Repo.Query()).AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        return entidad is null ? null : Convertir(entidad);
    }

    public async Task<TDto> CrearAsync(TDto dto)
    {
        dto.Id = 0;
        await ValidarAsync(dto);

        var entidad = Mapper.Map<TDto, TEntity>(dto);
        entidad.Id = 0;
        await Repo.AddAsync(entidad);
        await Uow.SaveChangesAsync();

        return (await ObtenerPorIdAsync(entidad.Id))!;
    }

    public async Task ActualizarAsync(int id, TDto dto)
    {
        dto.Id = id;
        await ValidarAsync(dto);

        var entidad = await Repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"No existe el registro con id {id}.");

        Mapper.Map(dto, entidad);
        entidad.Id = id;
        Repo.Update(entidad);
        await Uow.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var entidad = await Repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"No existe el registro con id {id}.");

        Repo.Delete(entidad);
        await Uow.SaveChangesAsync();
    }

    public async Task<List<LookupDto>> ObtenerLookupAsync()
    {
        var lista = await Incluir(Repo.Query()).AsNoTracking().OrderBy(e => e.Id).ToListAsync();
        return lista.Select(e => new LookupDto { Id = e.Id, Texto = Etiqueta(e) }).ToList();
    }
}
