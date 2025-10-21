using GestionHuacales.Shared;
using GestionHuacales.Shared.Dtos;

namespace GestionHuacales.BlazorWasm.Services;

public interface ITareasApiService
{
    Task<Resource<List<TareaResponse>>> GetTareasAsync();
    Task<Resource<TareaResponse>> CreateTareaAsync(TareaRequest request);
}