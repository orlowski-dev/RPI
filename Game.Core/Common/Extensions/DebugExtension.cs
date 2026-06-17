using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Game.Core.Extensions;

public static class DebugExtension
{
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
        Console.WriteLine(CreateLogContent(obj, msg, methodName));
    }

    [DoesNotReturn]
    public static void Fatal(this object obj, string msg, [CallerMemberName] string methodName = "")
    {
        var content = $"[{obj.GetType().Name}:{methodName}] {msg}";
        Console.WriteLine(content);
        throw new InvalidCastException(content);
    }

    public static Error UnknowError(string? msg = null)
    {
        return new(msg ?? "no message", ErrorType.Unknown);
    }
}
