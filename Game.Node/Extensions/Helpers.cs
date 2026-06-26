public static class Helpers
{
    public static string ConcatGodotId(Godot.Container uiElement, string id)
    {
        var elemPath = uiElement.GetPath();
        return string.Join("/", [elemPath, id]);
    }
}
