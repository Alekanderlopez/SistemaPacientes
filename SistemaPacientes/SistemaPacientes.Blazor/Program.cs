using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SistemaPacientes.Blazor;
using SistemaPacientes.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// IMPORTANTE: cambia el puerto por el de tu API (mira SistemaPacientes.API/Properties/launchSettings.json)
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7001/") });
builder.Services.AddScoped<ICrudClient, CrudClient>();

await builder.Build().RunAsync();
