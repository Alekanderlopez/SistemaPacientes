using SistemaPacientes.Entities.Models;

namespace SistemaPacientes.DAL.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> Query();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entidad);
    void Update(T entidad);
    void Delete(T entidad);   // borrado lógico
}
