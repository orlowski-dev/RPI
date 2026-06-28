using Godot;

[Tool]
public partial class EditorOnly : Node2D
{
    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
            QueueFree();
    }
}
