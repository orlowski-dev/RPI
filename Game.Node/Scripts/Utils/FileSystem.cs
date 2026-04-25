using Godot;

/// /// <summary>
/// Statyczna klasa odpowiedzialna za pracę z plikami.
/// </summary>
/// <remarks>
/// Implementacja wykorzystuje Godot.FileAccess
/// </remarks>
public static class FileSystem
{
    /// /// <summary>
    /// Metoda odpowiada za zapis danych do pliku. Tworzenie pliku jeśli plik nie istnieje lub dopisuje dane do istniejącego wskazanego pliku.
    /// </summary>
    /// <param name="path">Ścieżka do pliku</param>
    /// <param name="content">Zawartość do zapisanua</param>
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
