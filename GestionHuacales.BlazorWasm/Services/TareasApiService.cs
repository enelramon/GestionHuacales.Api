using System.Net.Http.Json;
using GestionHuacales.Shared;
using GestionHuacales.Shared.Dtos;

namespace GestionHuacales.BlazorWasm.Services;

public class TareasApiService(HttpClient httpClient) : ITareasApiService
{
    public async Task<Resource<List<TareaResponse>>> GetTareasAsync()
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<List<TareaResponse>>("api/Tareas");
            return new Resource<List<TareaResponse>>.Success(response ?? []);
        }
        catch (Exception ex)
        {
            return new Resource<List<TareaResponse>>.Error(ex.Message);
        }
    }

    public async Task<Resource<TareaResponse>> CreateTareaAsync(TareaRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/Tareas", request);
            response.EnsureSuccessStatusCode();
            var createdTarea = await response.Content.ReadFromJsonAsync<TareaResponse>();
            return new Resource<TareaResponse>.Success(createdTarea!);
        }
        catch (HttpRequestException ex)
        {
            return new Resource<TareaResponse>.Error($"Error de red: {ex.Message}");
        }
        catch (NotSupportedException)
        {
            return new Resource<TareaResponse>.Error("Respuesta inválida del servidor.");
        }
    }
}

