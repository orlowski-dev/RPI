using Godot;
using System;


public partial class Logger : BaseSingleton<Logger>, ILogger
{
	/// <summary>
	/// Metoda opowiadająca za zapis logu do pliku i wypisanie go w konsoli.
	/// </summary>
	/// <param name="level">Rodzaj logu</param>
	/// <param name="service">Nazwa serwisu/skryptu</param>
	/// <param name="message">Treść wiadomości</param>
	public void Write(LogLevel level, string service, string message)
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
