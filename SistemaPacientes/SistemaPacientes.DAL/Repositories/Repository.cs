using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Entities.Models;

namespace SistemaPacientes.DAL.Repositories;

public class Repository<T>(AppDbContext ctx) : IRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _set = ctx.Set<T>();

    public IQueryable<T> Query() => _set.AsQueryable();

    public Task<T?> GetByIdAsync(int id) => _set.FirstOrDefaultAsync(e => e.Id == id);

    public async Task AddAsync(T entidad) => await _set.AddAsync(entidad);

    public void Update(T entidad) => _set.Update(entidad);

    public void Delete(T entidad)
    {
        entidad.Activo = false;
        _set.Update(entidad);
    }
}
