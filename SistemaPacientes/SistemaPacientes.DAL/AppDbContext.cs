using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Entities.Models;

namespace SistemaPacientes.DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Aseguradora> Aseguradoras => Set<Aseguradora>();
    public DbSet<Especialidad> Especialidades => Set<Especialidad>();
    public DbSet<Sala> Salas => Set<Sala>();
    public DbSet<Medicamento> Medicamentos => Set<Medicamento>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Medico> Medicos => Set<Medico>();
    public DbSet<Cita> Citas => Set<Cita>();
    public DbSet<Consulta> Consultas => Set<Consulta>();
    public DbSet<Diagnostico> Diagnosticos => Set<Diagnostico>();
    public DbSet<Receta> Recetas => Set<Receta>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        // Sin borrado en cascada (usamos borrado lógico)
        foreach (var fk in mb.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).ToList())
            fk.DeleteBehavior = DeleteBehavior.Restrict;

        // Filtro global: solo registros Activo = true
        foreach (var tipo in mb.Model.GetEntityTypes().Where(t => typeof(BaseEntity).IsAssignableFrom(t.ClrType)).ToList())
        {
            var e = Expression.Parameter(tipo.ClrType, "e");
            var filtro = Expression.Lambda(Expression.Property(e, nameof(BaseEntity.Activo)), e);
            mb.Entity(tipo.ClrType).HasQueryFilter(filtro);
        }

        mb.Entity<Paciente>().HasIndex(p => p.Cedula);
        mb.Entity<Medico>().HasIndex(m => m.Exequatur);
    }
}
