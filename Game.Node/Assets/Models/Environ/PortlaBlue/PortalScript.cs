using Godot;
using Microsoft.Extensions.DependencyInjection;

public enum ZoneType
{
    Dungeon,
}

public partial class PortalScript : Node3D
{
    [Export]
    public ZoneType Zone { get; set; }

    private Area3D _area = null!;
    private PortalPresenter _portalPresenter = null!;

    public override void _Ready()
    {
        _portalPresenter = ServiceProviderHolder.Provider.GetRequiredService<PortalPresenter>();
        _area = GetNode<Area3D>("EventArea");
        _area.BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is not CharacterBody3D)
        {
            return;
        }

        switch (Zone)
        {
            case ZoneType.Dungeon:
                TeleportToDungeon();
                break;
        }
    }

    private void TeleportToDungeon()
    {
        GetTree().CallDeferred("change_scene_to_file", ScenePaths.DungeonTest);
    }
}
