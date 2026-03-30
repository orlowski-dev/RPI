using System;

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
