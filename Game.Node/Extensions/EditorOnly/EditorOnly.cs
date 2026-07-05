using Godot;

public partial class EditorOnly : Node3D
{
    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
            GetNode<Node3D>("%cube").QueueFree();
    }
}
