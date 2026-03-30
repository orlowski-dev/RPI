using Godot;

public static class FileSystem
{
    public static void Write(string path, string content)
    {
        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
        file.StoreString(content);
    }
}
