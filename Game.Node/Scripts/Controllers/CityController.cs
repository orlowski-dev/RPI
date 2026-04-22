using Godot;

public partial class CityController : Node
{
    private GameManager GameManager => GameManager.Instance;
    private CitySignals CitySignals => CitySignals.Instance;
    private Signals GlobalSignals => Signals.Instance;
    private Node2D _playerSpawnPoint;
    private StaticBody2D _dungeonPortal;
    private bool _changingScene = false; // flaga do zapobiegania wielokrotnej zmiany sceny na AreaEntered

    public override void _Ready()
    {
        _playerSpawnPoint = GetNode<Node2D>("PlayerSpawnPoint");
        _dungeonPortal = GetNode<StaticBody2D>("DungeonPortal");

        var playerNode = GameManager.GetPlayerNode(
            GameManager.PlayerCharacter.CharacterClass.NodeName,
            _playerSpawnPoint.Position
        );

        AddChild(playerNode);

        var playerArea = playerNode.GetNode<Area2D>("Area2D");
        playerArea.AreaEntered += _OnArea2dEntered;

        var playerCameraNode = GameManager.GetPlayerCameraNode();
        AddChild(playerCameraNode);

        CitySignals.EmitDataSender(new CityHudData(GameManager.PlayerCharacter));
    }

    private void _OnArea2dEntered(Area2D area)
    {
        if (_changingScene)
            return;

        var collideWith = area.GetParent().Name;

        if (collideWith == "DungeonPortal")
        {
            _changingScene = true;
            GlobalSignals.EmitGameStateChanged(new GameManagerData(GameState.Dungeon));
        }
    }
}
