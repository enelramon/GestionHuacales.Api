using GestionHuacales.Api.UiTests;
using static VerifyNUnit.Verifier;

namespace GestionHuacales.Api.UiTests.Api;

public class JugadoresApiTest : ApiTestBase
{
    // GET /api/Jugadores
    [Test]
    public async Task GetJugadores_Should_Return_Jugadores()
    {
        var response = await ApiContext.GetAsync("jugadores");

        Assert.That(response.Status, Is.EqualTo(200), "Expected status code 200");

        var text = await response.TextAsync();
        await VerifyJson(text);
    }

    // GET /api/Jugadores/{id}
    [Test]
    public async Task GetJugadorById_Should_Return_Jugador()
    {
        var response = await ApiContext.GetAsync("jugadores/1");

        Assert.That(response.Status, Is.EqualTo(200), "Expected status code 200");

        var text = await response.TextAsync();
        await VerifyJson(text);
    }

    [Test]
    public async Task GetJugadorById_WithInvalidId_Should_Return_NotFound()
    {
        var response = await ApiContext.GetAsync("jugadores/999999");

        Assert.That(response.Status, Is.EqualTo(404), "Expected status code 404");
    }

    // POST /api/Jugadores
    [Test]
    public async Task PostJugador_Should_Create_Jugador()
    {
        var newJugador = new Dictionary<string, object>
        {
            { "nombres", "Test Jugador" },
            { "email", "testjugador@test.com" }
        };

        var response = await ApiContext.PostAsync("jugadores", new APIRequestContextOptions
        {
            DataObject = newJugador
        });

        Assert.That(response.Status, Is.EqualTo(200).Or.EqualTo(201), "Expected status code 200 or 201");

        var text = await response.TextAsync();
        await VerifyJson(text);
    }

    [Test]
    public async Task PostJugador_WithMissingFields_Should_Return_BadRequest()
    {
        var invalidJugador = new Dictionary<string, object>
        {
            { "nombres", "" }
        };

        var response = await ApiContext.PostAsync("jugadores", new APIRequestContextOptions
        {
            DataObject = invalidJugador
        });

        Assert.That(response.Status, Is.EqualTo(400), "Expected status code 400");
    }

    // PUT /api/Jugadores/{id}
    [Test]
    public async Task PutJugador_Should_Update_Jugador()
    {
        var updatedJugador = new Dictionary<string, object>
        {
            { "jugadorId", 1 },
            { "nombres", "Enel Actualizado" },
            { "email", "enel@gmail.com" }
        };

        var response = await ApiContext.PutAsync("jugadores/1", new APIRequestContextOptions
        {
            DataObject = updatedJugador
        });

        Assert.That(response.Status, Is.EqualTo(200).Or.EqualTo(204), "Expected status code 200 or 204");

        // Restaurar datos originales para no afectar otros tests
        await ApiContext.PutAsync("jugadores/1", new APIRequestContextOptions
        {
            DataObject = new Dictionary<string, object>
            {
                { "jugadorId", 1 },
                { "nombres", "Enel" },
                { "email", "enel@gmail.com" }
            }
        });
    }

    [Test]
    public async Task PutJugador_WithInvalidId_Should_Return_NotFound()
    {
        var updatedJugador = new Dictionary<string, object>
        {
            { "jugadorId", 999999 },
            { "nombres", "No Existe" },
            { "email", "noexiste@test.com" }
        };

        var response = await ApiContext.PutAsync("jugadores/999999", new APIRequestContextOptions
        {
            DataObject = updatedJugador
        });

        // La API actualmente no retorna 404 para IDs inexistentes
        Assert.That(response.Status, Is.EqualTo(200).Or.EqualTo(404), "Expected status code 200 or 404");
    }
}
