namespace GestionHuacales.Shared.Dtos;

public record PartidaRequest(
    int Jugador1Id, 
    int Jugador2Id
);

public record PartidaResponse(
    int PartidaId,
    int Jugador1Id,
    int Jugador2Id
);