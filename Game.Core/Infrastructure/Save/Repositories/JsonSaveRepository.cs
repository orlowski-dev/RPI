using System.Text.Json;
using Game.Core.Infrastructure.Save.Contracts;
using Game.Core.Infrastructure.Save.Snapshots;

namespace Game.Core.Infrastructure.Save.Repositories;

public class JsonSaveRepository : ISaveRepository
{
    private const string _saveRoot = "save";

    public Result Save(GameSnapshot gameSnapshot)
    {
        var dirPath = Path.Combine(_saveRoot);
        if (!Directory.Exists(dirPath))
        {
            DebugExtension.Log(this, $"Creatring dir: {dirPath}.");
            Directory.CreateDirectory(dirPath);
        }

        var filePath = Path.Combine(_saveRoot, $"{gameSnapshot.Id}.json");

        SaveFile(file: filePath, data: gameSnapshot);

        return Result.Success();
    }

    private void SaveFile<T>(string file, T data)
    {
        var json = JsonSerializer.Serialize(
            data,
            new JsonSerializerOptions { WriteIndented = true }
        );

        DebugExtension.Log(this, $"Saving file: {file}");
        File.WriteAllText(file, json);
    }

    public Result<GameSnapshot> Load(string snapshotId)
    {
        var path = Path.Combine(_saveRoot, $"{snapshotId}.json");
        var json = File.ReadAllText(path);
        var snapshot = JsonSerializer.Deserialize<GameSnapshot>(json);

        if (snapshot is null)
        {
            return Result<GameSnapshot>.Fail(
                new(Message: "Cannot parse json file into GameSnapshot", Type: ErrorType.Validation)
            );
        }

        return Result<GameSnapshot>.Success(snapshot);
    }

    public Result<GameSnapshot> Load(Guid snapshotId)
    {
        return Load(snapshotId.ToString());
    }

    private IReadOnlyList<GameSnapshot> GetSaveFiles()
    {
        List<GameSnapshot> list = new();
        foreach (var file in Directory.GetFiles(_saveRoot))
        {
            var splitted = file.Split('.');
            if (splitted[splitted.Length - 1] != "json")
            {
                continue;
            }

            var save = Load(splitted[0]);

            if (save.IsFailure)
            {
                DebugExtension.Log(this, $"Cannot read save: {splitted[0]}. Skipping.");
                continue;
            }

            list.Add(save.Value);
        }

        return list;
    }

    public IReadOnlyCollection<GameSnapshot> List()
    {
        return GetSaveFiles();
    }
}
