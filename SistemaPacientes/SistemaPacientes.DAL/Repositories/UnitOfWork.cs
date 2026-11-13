using SistemaPacientes.Entities.Models;

namespace SistemaPacientes.DAL.Repositories;

public class UnitOfWork(AppDbContext ctx) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositorios = new();

    public IRepository<T> Repository<T>() where T : BaseEntity
    {
        if (!_repositorios.TryGetValue(typeof(T), out var repo))
        {
            repo = new Repository<T>(ctx);
            _repositorios[typeof(T)] = repo;
        }
        return (IRepository<T>)repo;
    }

    public Task<int> SaveChangesAsync() => ctx.SaveChangesAsync();
}
