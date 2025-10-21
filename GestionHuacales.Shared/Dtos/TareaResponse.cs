namespace GestionHuacales.Shared.Dtos;

public record TareaResponse(int Id, string Descripcion, bool EstaCompletada, DateTime FechaCreacion);