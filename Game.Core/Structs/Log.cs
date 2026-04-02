using System;

/// /// <summary>
/// Struktura reprezentująca pojedynczy wpis logu systemowego.
/// </summary>
/// <param name="Level">Rodzaj logu</param>
/// <param name="Message">Treść wiadomości</param>
/// <param name="Timestamp">Data i czas utworzenia</param>
/// <param name="Service">Nazwa systemu lub komponentu generującego log</param>
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

    /// /// <summary>
    /// Zwraca log w formacie jednej linii tekstu.
    /// </summary>
    /// <remarks>
    /// Format: Timestamp | Level | Service | Message
    /// </remarks>
    public string OneLine => $"{Timestamp.ToString()} | {Level} | {Service} | {Message}\n";
}
