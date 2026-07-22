using System.Net.Http.Json;
using GestionHuacales.Shared;
using GestionHuacales.Shared.Dtos;

namespace GestionHuacales.BlazorWasm.Services;

public interface IMovimientosApiService
{
    Task<Resource<MovimientoResponse[]>> GetMovimientosAsync(int partidaId);
    Task<Resource<bool>> PostMovimientoAsync(MovimientoRequest movimiento);
}

public class MovimientosApiService(HttpClient httpClient) : IMovimientosApiService
{
    public async Task<Resource<MovimientoResponse[]>> GetMovimientosAsync(int partidaId)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<MovimientoResponse[]>($"api/Movimientos/{partidaId}");
            return new Resource<MovimientoResponse[]>.Success(response ?? []);
        }
        catch (Exception ex)
        {
            return new Resource<MovimientoResponse[]>.Error(ex.Message);
        }
    }

    public async Task<Resource<bool>> PostMovimientoAsync(MovimientoRequest movimiento)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/Movimientos", movimiento);
            response.EnsureSuccessStatusCode();
            return new Resource<bool>.Success(true);
        }
        catch (HttpRequestException ex)
        {
            return new Resource<bool>.Error($"Error de red: {ex.Message}");
        }
        catch (NotSupportedException)
        {
            return new Resource<bool>.Error("Respuesta inválida del servidor.");
        }
    }
}
