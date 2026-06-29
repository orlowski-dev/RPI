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

    private EditorOnly[] _enemySpawnPoints = new EditorOnly[5];

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

        GetEnemySpawnPoints();
        SpawnEncounters();

        GD.Print(DebugExtension.Dump(_enemySpawnPoints[0].GetChildren().Count));
    }

    public override void _PhysicsProcess(double delta)
    {
        var old = _light.GlobalPosition;
        _light.Position = new Vector3(_playerNode.Position.X, old.Y, _playerNode.Position.Z);
    }

    private void GetEnemySpawnPoints()
    {
        for (var i = 0; i < 5; i++)
        {
            _enemySpawnPoints[i] = GetNode<EditorOnly>("EnemySpawn" + i);
        }
    }

    private void SpawnEncounters()
    {
        if (_dungeon is null)
            return;

        for (var i = 0; i < _dungeon.Encounters.Count; i++)
        {
            var current = _dungeon.Encounters[i];
            foreach (var enemy in current.Enemies)
            {
                var enemyModel = GD.Load<PackedScene>(enemy.NodePath);
                var model = enemyModel.Instantiate<Node3D>();
                model.Position = new Vector3(
                    _enemySpawnPoints[i].Position.X,
                    0,
                    _enemySpawnPoints[i].Position.Z
                );
                AddChild(model);
            }
        }
    }
}
