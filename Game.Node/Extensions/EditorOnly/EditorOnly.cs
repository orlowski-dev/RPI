using Godot;

[Tool]
public partial class EditorOnly : Node
{
    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
            QueueFree();
    }
}
