namespace Game.Core.Application.Results;

public static class Log
{
    public static bool Debug { get; } = true;

    public static void Write(this object obj, string msg)
    {
        Console.WriteLine($"[{obj.GetType().Name}] {msg}");
    }
}
