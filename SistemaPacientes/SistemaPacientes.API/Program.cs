using Microsoft.EntityFrameworkCore;
using SistemaPacientes.API.Middleware;
using SistemaPacientes.BLL;
using SistemaPacientes.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDal(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddBll();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(o => o.AddPolicy("Blazor", p =>
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCors("Blazor");
app.UseAuthorization();
app.MapControllers();

app.Run();
