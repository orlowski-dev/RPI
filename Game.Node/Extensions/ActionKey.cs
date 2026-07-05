using Godot;

public static class ActionKey
{
    public static Key? GetKeycode(string action)
    {
        foreach (var e in InputMap.ActionGetEvents(action))
        {
            if (e is InputEventKey key)
            {
                return key.Keycode != Key.None ? key.Keycode : key.PhysicalKeycode;
            }
        }
        return null;
    }

    public static string? GetLabel(string action)
    {
        var keycode = GetKeycode(action);
        return keycode is null ? null : OS.GetKeycodeString(keycode.Value);
    }
}
