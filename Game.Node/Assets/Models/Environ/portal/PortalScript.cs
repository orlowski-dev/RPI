using Godot;

public partial class PortalScript : Node3D
{
    public enum Type
    {
        Dungeon,
        City,
    }

    [Export]
    public Type PortalType;

    [Export]
    public bool Enabled = true;

    private Area3D _eventArea = null!;

    public override void _Ready()
    {
        _eventArea = GetNode<Area3D>("%EventArea");
        _eventArea.BodyEntered += OnBodyEntered;
    }

    public override void _ExitTree()
    {
        _eventArea.BodyEntered -= OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is PlayerController && Enabled)
        {
            if (PortalType == Type.Dungeon)
            {
                GetTree().CallDeferred("change_scene_to_file", ScenePaths.DungeonTest);
            }
            if (PortalType == Type.City)
            {
                GetTree().CallDeferred("change_scene_to_file", ScenePaths.City);
            }
        }
    }
}
