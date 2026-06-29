using Godot;

[Tool]
public partial class EditorOnly : Node3D
{
    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
            QueueFree();
    }
}
