namespace GestionHuacales.Shared;

public record Tarea(
    int TareaId, 
    string Descripcion,
    bool EstaCompletada
);