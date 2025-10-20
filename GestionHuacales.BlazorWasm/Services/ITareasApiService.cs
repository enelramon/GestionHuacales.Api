using GestionHuacales.Shared;

namespace GestionHuacales.BlazorWasm.Services;

public interface ITareasApiService
{
    Task<Resource<List<TareaResponse>>> GetTareasAsync();
    Task<Resource<TareaResponse>> CreateTareaAsync(TareaRequest request);
}