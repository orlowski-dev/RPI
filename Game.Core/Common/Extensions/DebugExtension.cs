using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;

public static class DebugExtension
{
    private static readonly string LogFilePath = Path.Combine(
        AppContext.BaseDirectory,
        "debug_log.txt"
    );

    public static string Dump(this object obj)
    {
        return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
    }

    public static string CreateLogContent(
        this object obj,
        string msg,
        [CallerMemberName] string methodName = ""
    )
    {
        return $"[{obj.GetType().Name}:{methodName}] {msg}";
    }

    public static void Log(this object obj, string msg, [CallerMemberName] string methodName = "")
    {
        var content = CreateLogContent(obj, msg, methodName);
        Console.WriteLine(content);
        WriteToFile(content);
    }

    [DoesNotReturn]
    public static void Fatal(this object obj, string msg, [CallerMemberName] string methodName = "")
    {
        var content = $"[{obj.GetType().Name}:{methodName}] {msg}";
        Console.WriteLine(content);
        WriteToFile($"FATAL: {content}");
        throw new Exception(content);
    }

    private static void WriteToFile(string content)
    {
        try
        {
            File.AppendAllText(
                LogFilePath,
                $"{DateTime.Now:HH:mm:ss.fff} {content}{Environment.NewLine}"
            );
        }
        catch
        {
            // ignoruj błędy zapisu, nie chcemy crashować gry przez logger
        }
    }

    public static Error UnknowError(string? msg = null)
    {
        return new(msg ?? "no message", ErrorType.Unknown);
    }
}
