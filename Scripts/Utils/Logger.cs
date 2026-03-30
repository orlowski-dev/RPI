using System;
using Godot;

public static class Logger
{
    public static void Write(LogLevel level, string service, string message)
    {
        var log = new Log(
            level: level,
            service: service,
            message: message,
            timestamp: DateTime.Now
        );

        FileSystem.Write("user://logs/log.txt", log.OneLine);

        switch (level)
        {
            case LogLevel.Info:
                GD.Print(log.OneLine);
                break;
            case LogLevel.Warning:
                GD.PushWarning(log.OneLine);
                break;
            default:
                GD.PushError(log.OneLine);
                break;
        }
    }
}
