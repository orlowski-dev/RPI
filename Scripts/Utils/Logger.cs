using System;
using Godot;

public enum LogLevel
{
    Info,
    Warning,
    Error,
}

public readonly struct Log
{
    public LogLevel Level { get; init; }
    public string Message { get; init; }
    public DateTime Timestamp { get; init; }
    public string Service { get; init; }

    public Log(LogLevel level, string message, DateTime timestamp, string service)
    {
        Level = level;
        Message = message;
        Timestamp = timestamp;
        Service = service;
    }

    public string OneLine => $"{Timestamp.ToString()} | {Level} | {Service} | {Message}";
}

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
