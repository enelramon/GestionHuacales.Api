namespace GestionHuacales.Api.UiTests;

public static class Logger
{
    private static IList<string>? _collector;
    public static void SetCollector(IList<string> collector) => _collector = collector;
    public static void ClearCollector() => _collector?.Clear();
    public static void Info(string message)
    {
        var formatted = $"##[command][{DateTime.Now:HH:mm:ss}] {message}";
        Console.WriteLine(formatted);
        _collector?.Add(formatted);
    }
    public static void Warning(string message)
    {
        var formatted = $"##[warning][{DateTime.Now:HH:mm:ss}] {message}";
        Console.WriteLine(formatted);
        _collector?.Add(formatted);
    }
    public static void Error(string message)
    {
        var formatted = $"##[error][{DateTime.Now:HH:mm:ss}] {message}";
        Console.WriteLine(formatted);
        _collector?.Add(formatted);
    }
}