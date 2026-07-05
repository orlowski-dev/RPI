using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class DungeonScene : Node
{
	private IGameSessionProvider _gsProvider = null!;
	private DungeonPresenter _dungPresenter = null!;
	private Player _player => _gsProvider.Current!.Player;
	private Node3D _playerNode = null!;
	private Dungeon? _dungeon => _gsProvider?.Current?.Dungeon;
	private DungeonView _view = null!;

	private EditorOnly[] _enemySpawnPoints = null!;

	public override void _Ready()
	{
		_gsProvider = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
		_dungPresenter = ServiceProviderHolder.Provider.GetRequiredService<DungeonPresenter>();

		_playerNode = new PlayerSpawner().GetNode(_player.NodePath);
		_view = GetNode<DungeonView>("CanvasLayer/DungeonView");
		// create dung
		_dungPresenter.OnViewReady();

		AddChild(_playerNode);
		AddChild(new FollowCameraSpawner().GetNode());

		if (_dungeon is null)
		{
			DebugExtension.Fatal(this, "Dungeon in game session is null!");
		}

		_enemySpawnPoints = new EditorOnly[_dungeon.Encounters.Count];

		GetEnemySpawnPoints();
		SpawnEncounters();
		_view.InitUI();
	}

	public override void _PhysicsProcess(double delta) { }

	private void GetEnemySpawnPoints()
	{
		for (var i = 0; i < _enemySpawnPoints.Count(); i++)
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
			var enemyIndex = 0;
			foreach (var enemy in current.Enemies)
			{
				var enemyNode = EnemySpawner.GetNode(enemy.NodePath);
				enemyNode.Encounter = current;
				enemyNode.Enemy = enemy;
				var spp = _enemySpawnPoints[i].Position;
				var offset = new Vector3(enemyIndex * 1.5f, 0, 0); // odstęp między wrogami
				enemyNode.Position = new Vector3(spp.X + offset.X, 0, spp.Z + offset.Z);

				AddChild(enemyNode);
				enemyNode.Label = enemy.IsAlive ? enemy.DisplayName : "";
				if (!enemy.IsAlive)
				{
					enemyNode.DisableCollisions();
				}
				enemyIndex++;
			}
		}
	}
}
