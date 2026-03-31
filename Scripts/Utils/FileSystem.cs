using Godot;

public static class FileSystem
{
	public static void Write(string path, string content)
	{
		var fileExists = false;

		if (FileAccess.FileExists(path))
		{
			fileExists = true;
		}

		// ustawia flagę otwarcia pliku - jak plik istnieje to dopisz a jak nie
		// to utwórz i wpisz
		var flag = fileExists ? FileAccess.ModeFlags.ReadWrite : FileAccess.ModeFlags.Write;

		using var file = FileAccess.Open(path, flag);
		if (fileExists)
			file.SeekEnd(); // ustawia kursor na koniec
		file.StoreString(content);
	}
}
