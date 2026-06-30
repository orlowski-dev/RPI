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
	private SpotLight3D _participantLight = null!;

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

		_targetLight = GetNode<SpotLight3D>("%TargetLight");
		_participantLight = GetNode<SpotLight3D>("%CurrentParticipantLight");

		SpawnModels();
		MoveParticipantLight();
		MoveTargetLight();
	}

	private void SpawnModels()
	{
		for (var i = 0; i < Combat.Participants.Count; i++)
		{
			var currentPart = Combat.Participants[i];
			var model = GD.Load<PackedScene>(currentPart.NodePath).Instantiate<Node3D>();
			var currentSpawn = _spawnPoints[(Sp)i];

			model.Position = new Vector3(currentSpawn.Position.X, 0, currentSpawn.Position.Z);
			_actorsMap[currentPart] = (Sp)i;
			model.RotateY(currentSpawn.Rotation.Y);

			currentSpawn.OnClicked += () => OnClickOnEnemy(currentPart);

			AddChild(model);
		}
	}

	private void OnClickOnEnemy(Actor actor)
	{
		Combat.SetTarget(actor);
		MoveParticipantLight();
		MoveTargetLight();
	}

	private void MoveParticipantLight()
	{
		var targActor = _actorsMap[Combat.ActiveParticipant];
		var targSlotPos = _spawnPoints[targActor].Position;
		_participantLight.Position = new Vector3(
			targSlotPos.X,
			_participantLight.Position.Y,
			targSlotPos.Z
		);
	}

	private void MoveTargetLight()
	{
		if (Combat.Target is null)
		{
			_targetLight.Visible = false;
			return;
		}

		_targetLight.Visible = true;
		var targActor = _actorsMap[Combat.Target];
		var targSlotPos = _spawnPoints[targActor].Position;
		_targetLight.Position = new Vector3(targSlotPos.X, _targetLight.Position.Y, targSlotPos.Z);
	}
}
