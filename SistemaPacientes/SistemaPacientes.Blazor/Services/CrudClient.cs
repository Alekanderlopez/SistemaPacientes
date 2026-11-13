using System.Net.Http.Json;
using System.Text.Json;
using SistemaPacientes.Entities.DTOs;

namespace SistemaPacientes.Blazor.Services;

public interface ICrudClient
{
    Task<List<T>> ListarAsync<T>(string endpoint);
    Task<List<LookupDto>> LookupAsync(string endpoint);
    Task<string?> CrearAsync<T>(string endpoint, T dto);
    Task<string?> ActualizarAsync<T>(string endpoint, int id, T dto);
    Task<string?> EliminarAsync(string endpoint, int id);
}

/// <summary>Cliente HTTP genérico. Los métodos de escritura devuelven null si todo salió bien, o el mensaje de error.</summary>
public class CrudClient(HttpClient http) : ICrudClient
{
    public async Task<List<T>> ListarAsync<T>(string endpoint) =>
        await http.GetFromJsonAsync<List<T>>($"api/{endpoint}") ?? new List<T>();

    public async Task<List<LookupDto>> LookupAsync(string endpoint) =>
        await http.GetFromJsonAsync<List<LookupDto>>($"api/{endpoint}/lookup") ?? new List<LookupDto>();

    public async Task<string?> CrearAsync<T>(string endpoint, T dto) =>
        await LeerError(await http.PostAsJsonAsync($"api/{endpoint}", dto));

    public async Task<string?> ActualizarAsync<T>(string endpoint, int id, T dto) =>
        await LeerError(await http.PutAsJsonAsync($"api/{endpoint}/{id}", dto));

    public async Task<string?> EliminarAsync(string endpoint, int id) =>
        await LeerError(await http.DeleteAsync($"api/{endpoint}/{id}"));

    private static async Task<string?> LeerError(HttpResponseMessage respuesta)
    {
        if (respuesta.IsSuccessStatusCode) return null;

        try
        {
            using var doc = JsonDocument.Parse(await respuesta.Content.ReadAsStringAsync());
            if (doc.RootElement.TryGetProperty("title", out var titulo))
            {
                var mensaje = titulo.GetString() ?? "Error";
                if (doc.RootElement.TryGetProperty("errors", out var errores))
                {
                    var detalle = errores.EnumerateObject()
                        .SelectMany(e => e.Value.EnumerateArray().Select(v => v.GetString()));
                    mensaje += " " + string.Join(" ", detalle);
                }
                return mensaje;
            }
        }
        catch (JsonException) { }

        return $"Error {(int)respuesta.StatusCode} al comunicarse con la API.";
    }
}
