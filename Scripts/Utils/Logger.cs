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

        GD.Print(log.OneLine);
    }
}
