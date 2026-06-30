using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class ArenaScene : Node3D
{
	private enum Sp
	{
		Player,
		Enemy0,
		Enemy1,
		Enemy2,
	}

	private IGameSessionProvider _gameSessionProvider = null!;
	private ArenaPresenter _arenaPresenter = null!;
	private Player Player => _gameSessionProvider.Current!.Player;
	private CombatSession Combat => _gameSessionProvider.Current!.CombatSession!;
	private SpotLight3D _targetLight = null!;

	private Dictionary<Sp, EditorOnly> _spawnPoints = new();
	private Dictionary<Actor, Sp> _actorsMap = new();

	public override void _Ready()
	{
		_gameSessionProvider =
			ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
		_arenaPresenter = ServiceProviderHolder.Provider.GetRequiredService<ArenaPresenter>();

		if (_gameSessionProvider.Current is null)
		{
			DebugExtension.Fatal(this, "Current gamesession is null!");
			return;
		}

		var session = _arenaPresenter.OnViewReady(Player);

		if (_gameSessionProvider.Current.CombatSession is null)
		{
			DebugExtension.Fatal(this, "Combat session in game session is null!");
			return;
		}

		_spawnPoints = new()
		{
			[Sp.Player] = GetNode<EditorOnly>("PlayerSpawn"),
			[Sp.Enemy0] = GetNode<EditorOnly>("EnemySpawn0"),
			[Sp.Enemy1] = GetNode<EditorOnly>("EnemySpawn1"),
			[Sp.Enemy2] = GetNode<EditorOnly>("EnemySpawn2"),
		};

		_targetLight = GetNode<SpotLight3D>("TargetLight");

		SpawnModels();
		MoveTargetLight();
	}

	private void SpawnModels()
	{
		for (var i = 0; i < Combat.Participants.Count; i++)
		{
			var model = GD.Load<PackedScene>(Combat.Participants[i].NodePath).Instantiate<Node3D>();
			model.Position = new Vector3(
				_spawnPoints[(Sp)i].Position.X,
				0,
				_spawnPoints[(Sp)i].Position.Z
			);
			_actorsMap[Combat.Participants[i]] = (Sp)i;
			model.RotateY(_spawnPoints[(Sp)i].Rotation.Y);
			AddChild(model);
		}
	}

	private void MoveTargetLight()
	{
		var targActor = _actorsMap[Combat.ActiveParticipant];
		var targSlotPos = _spawnPoints[targActor].Position;
		_targetLight.Position = new Vector3(targSlotPos.X, _targetLight.Position.Y, targSlotPos.Z);
	}
}
