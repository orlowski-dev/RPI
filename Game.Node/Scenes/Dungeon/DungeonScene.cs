using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class DungeonScene : Node
{
    private SpotLight3D _light = null!;
    private IGameSessionProvider _gsProvider = null!;
    private DungeonPresenter _dungPresenter = null!;
    private Player _player => _gsProvider.Current!.Player;
    private Node3D _playerNode = null!;
    private Dungeon? _dungeon => _gsProvider?.Current?.Dungeon;

    public override void _Ready()
    {
        _gsProvider = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
        _dungPresenter = ServiceProviderHolder.Provider.GetRequiredService<DungeonPresenter>();
        _light = GetNode<SpotLight3D>("Light");

        _playerNode = new PlayerSpawner().GetNode(_player.NodePath);

        // create dung
        _dungPresenter.OnViewReady();

        AddChild(_playerNode);
        AddChild(new FollowCameraSpawner().GetNode());

        SpawnEncounters();
    }

    public override void _PhysicsProcess(double delta)
    {
        var old = _light.GlobalPosition;
        _light.Position = new Vector3(_playerNode.Position.X, old.Y, _playerNode.Position.Z);
    }

    private void SpawnEncounters()
    {
        if (_dungeon is null)
            return;

        foreach (var encounter in _dungeon.Encounters)
        {
            GD.Print(string.Join(',', encounter.Enemies));
        }
    }
}
