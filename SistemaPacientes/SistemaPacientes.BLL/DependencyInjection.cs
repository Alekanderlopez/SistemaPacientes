using Microsoft.Extensions.DependencyInjection;
using SistemaPacientes.BLL.Interfaces;
using SistemaPacientes.BLL.Services;

namespace SistemaPacientes.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBll(this IServiceCollection services)
    {
        services.AddScoped<IAseguradoraService, AseguradoraService>();
        services.AddScoped<IEspecialidadService, EspecialidadService>();
        services.AddScoped<ISalaService, SalaService>();
        services.AddScoped<IMedicamentoService, MedicamentoService>();
        services.AddScoped<IPacienteService, PacienteService>();
        services.AddScoped<IMedicoService, MedicoService>();
        services.AddScoped<ICitaService, CitaService>();
        services.AddScoped<IConsultaService, ConsultaService>();
        services.AddScoped<IDiagnosticoService, DiagnosticoService>();
        services.AddScoped<IRecetaService, RecetaService>();
        return services;
    }
}
