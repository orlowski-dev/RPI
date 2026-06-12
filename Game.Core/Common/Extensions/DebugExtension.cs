using System.Text.Json;

namespace Game.Core.Extensions;

public static class DebugExtension
{
    public static string Dump(this object obj)
    {
        return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
    }
}
