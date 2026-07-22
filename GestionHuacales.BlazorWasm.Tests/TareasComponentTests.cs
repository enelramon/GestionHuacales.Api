using Bunit;
using GestionHuacales.BlazorWasm.Pages;
using GestionHuacales.BlazorWasm.Services;
using GestionHuacales.Shared;
using GestionHuacales.Shared.Dtos;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace GestionHuacales.BlazorWasm.Tests;

public class TareasComponentTests : TestContext
{
    private readonly ITareasApiService _mockApiService;

    public TareasComponentTests()
    {
        _mockApiService = Substitute.For<ITareasApiService>();

        Services.AddSingleton(_mockApiService);
    }

    [Fact]
    public void ComponenteMuestraListaDeTareas_CuandoApiRespondeExitosamente()
    {
        // Arrange
        var tareasDePrueba = new List<TareaResponse>
        {
            new(1, "Crear componente", false, DateTime.Now),
            new(2, "Hacer pruebas", true, DateTime.Now)
        };

        // Configuramos el método para que devuelva un resultado exitoso.
        _mockApiService.GetTareasAsync()
            .Returns(new Resource<List<TareaResponse>>.Success(tareasDePrueba));

        // Act
        var cut = RenderComponent<TareasComponent>();

        // Assert
        var itemsLi = cut.FindAll("li");
        Assert.Equal(2, itemsLi.Count);
        Assert.Contains("1 - Crear componente", itemsLi[0].TextContent);
    }

    [Fact]
    public void AgregarTarea_LlamaApiYActualizaLaLista()
    {
        // Arrange
        var nuevaTareaResponse = new TareaResponse(1, "Tarea desde test", false, DateTime.Now);

        // 1. Configuramos las llamadas en secuencia para GetTareasAsync.
        _mockApiService.GetTareasAsync().Returns(
            new Resource<List<TareaResponse>>.Success(new()), // Primera llamada (inicial)
            new Resource<List<TareaResponse>>.Success(new() { nuevaTareaResponse }) // Segunda llamada (tras crear)
        );

        // 2. Configuramos la llamada de creación.
        _mockApiService.CreateTareaAsync(Arg.Any<TareaRequest>())
            .Returns(new Resource<TareaResponse>.Success(nuevaTareaResponse));

        var cut = RenderComponent<TareasComponent>();

        // Act
        cut.Find("input").Change("Tarea desde test");
        cut.Find("button").Click();

        // Assert
        cut.WaitForState(() => cut.FindAll("li").Count == 1);
        Assert.Contains("1 - Tarea desde test", cut.Find("li").TextContent);
    }
}
