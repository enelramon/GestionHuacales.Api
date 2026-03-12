namespace GestionHuacales.Api.UiTests;


[Parallelizable(ParallelScope.Self)]
[TestFixture]
public abstract class ApiTestBase : PlaywrightTest, IDisposable, IAsyncDisposable
{
    private int _screenshotCounter = 1;
    private static readonly string ScreenshotFolderPath =
        Path.Combine(TestContext.CurrentContext.WorkDirectory, "Attachments");

    public IAPIRequestContext ApiContext = null!;
    #region Settings

    protected static string BaseUrl = Environment.GetEnvironmentVariable("BASE_URL") ?? "https://gestionhuacalesapi.azurewebsites.net/api/";
    protected static string Username = Environment.GetEnvironmentVariable("USER_NAME") ?? "uat";
    protected static string Password = Environment.GetEnvironmentVariable("PASSWORD") ?? "uat2024";


    protected static float DefaultTimeout => float.TryParse(Environment.GetEnvironmentVariable("defaultTimeout"), out var timeout) ? timeout : 60000f;
    #endregion

    private List<string> _logEntries = new();
    private bool _disposed = false;

    [SetUp]
    public void Setup()
    {
        ApiContext = Playwright.APIRequest.NewContextAsync( new APIRequestNewContextOptions
        {
            BaseURL = BaseUrl,
            Timeout = DefaultTimeout,
            ExtraHTTPHeaders = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
            }
        }).GetAwaiter().GetResult();

        _logEntries = new();
        Logger.SetCollector(_logEntries);
        _screenshotCounter = 1;
        var className = TestContext.CurrentContext.Test.ClassName ?? "className";
        Console.WriteLine($"##[debug] ══════════════▶ Running {className} ◀══════════════");
        Console.WriteLine($"##[group] {className}");
    }

    [TearDown]
    public async Task TearDown()
    {
        try
        {
            // Take screenshot and capture details if test failed
            await ApiContext.DisposeAsync() ;
            Console.WriteLine("##[endgroup]");
        }
        catch
        {
            throw;
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        // no unmanaged resources to dispose
          ApiContext.DisposeAsync().GetAwaiter().GetResult();
        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        // no unmanaged resources to dispose asynchronously
        await ApiContext.DisposeAsync();

        _disposed = true;
        await Task.CompletedTask;
    }
}