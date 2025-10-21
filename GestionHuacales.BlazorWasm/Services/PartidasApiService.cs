using System.Net.Http.Json;
using GestionHuacales.Shared;
using GestionHuacales.Shared.Dtos;

namespace GestionHuacales.BlazorWasm.Services;

public interface IPartidasApiService
{
    Task<Resource<List<PartidaResponse>>> GetPartidasAsync();
    Task<Resource<PartidaResponse>> GetPartidaAsync(int partidaId);
    Task<Resource<PartidaResponse>> PostPartida(int jugador1, int jugador2);
}

public class PartidasApiService(HttpClient httpClient) : IPartidasApiService
{
    public async Task<Resource<List<PartidaResponse>>> GetPartidasAsync()
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<List<PartidaResponse>>("api/Partidas");
            return new Resource<List<PartidaResponse>>.Success(response ?? []);
        }
        catch (Exception ex)
        {
            return new Resource<List<PartidaResponse>>.Error(ex.Message);
        }
    }
    public async Task<Resource<PartidaResponse>> GetPartidaAsync(int partidaId)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<PartidaResponse>($"api/Partidas/{partidaId}");
            return new Resource<PartidaResponse>.Success(response!);
        }
        catch (Exception ex)
        {
            return new Resource<PartidaResponse>.Error(ex.Message);
        }
    }
    public async Task<Resource<PartidaResponse>> PostPartida(int jugador1, int jugador2)
    {
        var request = new PartidaRequest(jugador1, jugador2);
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/Partidas", request);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<PartidaResponse>();
            return new Resource<PartidaResponse>.Success(created!);
        }
        catch (HttpRequestException ex)
        {
            return new Resource<PartidaResponse>.Error($"Error de red: {ex.Message}");
        }
        catch (NotSupportedException)
        {
            return new Resource<PartidaResponse>.Error("Respuesta inválida del servidor.");
        }
    } 
}