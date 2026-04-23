using Godot;

public partial class LevelController : Node
{
	private GameManager GameManager => GameManager.Instance;
	private LevelSignals LevelSignals => LevelSignals.Instance;
	private Signals GlobalSignals => Signals.Instance;
	private Node2D _playerSpawnPoint;
	private bool _changingScene = false; // flaga do zapobiegania wielokrotnej zmiany sceny na AreaEntered

	public override void _Ready()
	{
		_playerSpawnPoint = GetNode<Node2D>("PlayerSpawnPoint");

		var playerNode = GameManager.GetPlayerNode(
			GameManager.PlayerCharacter.CharacterClass.NodeName,
			_playerSpawnPoint.Position
		);

		AddChild(playerNode);

		var playerArea = playerNode.GetNode<Area2D>("Area2D");
		playerArea.AreaEntered += _OnArea2dEntered;

		var playerCameraNode = GameManager.GetPlayerCameraNode();
		AddChild(playerCameraNode);

		if (Name == "Dungeon")
		{
			var spawnPoint = GetNode<Node2D>("EnemySpawnPoint0");
			var enemy = GameManager.GetEnemy(spawnPoint.Position);
			AddChild(enemy);
		}

		LevelSignals.EmitDataSender(new CityHudData(GameManager.PlayerCharacter));
	}

	private void _OnArea2dEntered(Area2D area)
	{
		if (_changingScene)
			return;

		var collideWith = area.GetParent();

		if (collideWith.Name == "DungeonPortal")
		{
			_changingScene = true;
			GlobalSignals.EmitGameStateChanged(new GameManagerData(GameState.Dungeon));
		}

		if (collideWith.IsInGroup("Enemy"))
		{
			GD.Print("COllide with enemy!");
		}
	}
}
