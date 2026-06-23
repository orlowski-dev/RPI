using System.Text.Json;

public class JsonSaveRepository : ISaveRepository
{
    private const string _saveRoot = "save";
    private const string _meta = "meta.json";

    private void MakeSaveDir()
    {
        var dirPath = Path.Combine(_saveRoot);
        if (!Directory.Exists(dirPath))
        {
            DebugExtension.Log(this, $"Creatring dir: {dirPath}.");
            Directory.CreateDirectory(dirPath);
        }
    }

    public Result Save(GameSnapshot gameSnapshot)
    {
        MakeSaveDir();
        var filePath = Path.Combine(_saveRoot, $"{gameSnapshot.Id}.json");

        SaveFile(file: filePath, data: gameSnapshot);
        SaveMeta(new MetaMapper().ToSnapshot(gameSnapshot.Id));

        return Result.Success();
    }

    private void SaveFile<T>(string file, T data)
    {
        var json = JsonSerializer.Serialize(
            data,
            new JsonSerializerOptions { WriteIndented = true }
        );

        // DebugExtension.Log(this, $"Saving file: {file}");
        File.WriteAllText(file, json);
    }

    private Result<T> LoadJsonFile<T>(string fileName)
    {
        var path = Path.Combine(_saveRoot, fileName);
        if (!new FileInfo(path).Exists)
        {
            return Result<T>.Fail(new($"Cannot find file: {path}.", ErrorType.IOError));
        }

        var json = File.ReadAllText(path);
        var snapshot = JsonSerializer.Deserialize<T>(json);

        if (snapshot is null)
        {
            return Result<T>.Fail(
                new(
                    Message: $"Cannot parse json file into {typeof(T)}.",
                    Type: ErrorType.Validation
                )
            );
        }

        return Result<T>.Success(snapshot);
    }

    public Result<GameSnapshot> Load(string snapshotId)
    {
        return LoadJsonFile<GameSnapshot>($"{snapshotId}.json");
    }

    public Result SaveMeta(MetaSnapshot snapshot)
    {
        MakeSaveDir();
        var path = Path.Combine(_saveRoot, _meta);
        SaveFile<MetaSnapshot>(path, snapshot);
        return Result.Success();
    }

    public Result<MetaSnapshot> LoadMeta()
    {
        return LoadJsonFile<MetaSnapshot>(_meta);
    }

    public Result<GameSnapshot> Load(Guid snapshotId)
    {
        return Load(snapshotId.ToString());
    }

    private IReadOnlyList<GameSnapshot> GetSavedFiles()
    {
        List<GameSnapshot> list = new();
        foreach (var entry in Directory.GetFileSystemEntries(_saveRoot))
        {
            var info = new FileInfo(entry);
            if (info.Attributes != FileAttributes.Normal || info.Extension != ".json")
                continue;

            var name = info.Name.Split(info.Extension)[0];
            if (name.Length == 0)
                continue;

            var savedata = Load(name);

            if (savedata.IsFailure)
            {
                DebugExtension.Log(this, $"Cannot read save: {name}. Skipping.");
                continue;
            }

            list.Add(savedata.Value);
        }

        return list;
    }

    public IReadOnlyList<GameSnapshot> List()
    {
        return GetSavedFiles();
    }
}
