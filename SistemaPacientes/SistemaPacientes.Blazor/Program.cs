using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SistemaPacientes.Blazor;
using SistemaPacientes.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7033/") });
builder.Services.AddScoped<ICrudClient, CrudClient>();

await builder.Build().RunAsync();