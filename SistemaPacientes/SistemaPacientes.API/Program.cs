using Microsoft.EntityFrameworkCore;
using SistemaPacientes.API.Middleware;
using SistemaPacientes.BLL;
using SistemaPacientes.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Registro de dependencias por capa
builder.Services.AddDal(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddBll();

// Manejo global de errores
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// CORS para que el proyecto Blazor pueda consumir la API
builder.Services.AddCors(o => o.AddPolicy("Blazor", p =>
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Crea/actualiza la base de datos automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
}

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("Blazor");
app.UseAuthorization();
app.MapControllers();

app.Run();
