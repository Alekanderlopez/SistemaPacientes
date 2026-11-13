using SistemaPacientes.Entities.Models;

namespace SistemaPacientes.DAL.Repositories;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : BaseEntity;
    Task<int> SaveChangesAsync();
}
