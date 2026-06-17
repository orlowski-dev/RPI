using System.Text.Json;
using Game.Core.Domain.Save;
using Game.Core.Infrastructure.Save.Contracts;

namespace Game.Core.Infrastructure.Save.Repositories;

public class JsonSaveRepository : ISaveRepository
{
    private const string _saveRoot = "temp";
    private const string _slot = "slot1";

    // generyczny żeby zachować runtime type.. bez tego w json.serialize musi być snapshot.GetType() :)
    public Result Save<T>(T snapshot)
        where T : ISnapshot
    {
        var dirPath = Path.Combine(_saveRoot, _slot);
        if (!Directory.Exists(dirPath))
        {
            DebugExtension.Log(this, $"Creatring dir: {dirPath}.");
            Directory.CreateDirectory(dirPath);
        }

        var filePath = Path.Combine(dirPath, "save.json");
        var json = JsonSerializer.Serialize(
            snapshot,
            new JsonSerializerOptions { WriteIndented = true }
        );

        DebugExtension.Log(this, $"Saving file: {filePath}");
        File.WriteAllText(filePath, json);

        return Result.Success();
    }

    public void Load()
    {
        // przyj slotid
        // zwraca snapshot
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<SaveSlot> List()
    {
        throw new NotImplementedException();
    }
}
