using Godot;

public partial class GameManager : BaseSingleton<GameManager>
{
    private GameService _service;
    private string _scriptName;
    private Signals _signals => Signals.Instance;
    private Logger _logger => Logger.Instance;

    public override void _Ready()
    {
        _scriptName = this.GetType().Name;
        _service = new(logger: _logger);

        _signals.GameStateChanged += OnGameStateChanged;

        _logger.Write(LogLevel.Info, _scriptName, "GameManager loaded.");
    }

    public override void _ExitTree()
    {
        _signals.GameStateChanged -= OnGameStateChanged;
    }

    /// <summary>
    /// Zwraca wartość zmiennej PlayerCharacter z _service.
    /// </summary>
    public PlayerCharacter PlayerCharacter => _service.PlayerCharacter;

    public EnemyCharacter EnemyCharacter => _service.EnemyCharacter;

    private void OnGameStateChanged(GameManagerData data)
    {
        var scenePath = _service.GetScenePath(data);
        // GetTree().ChangeSceneToFile(scenePath);
        GetTree().CallDeferred("change_scene_to_file", scenePath); // bo jak wywyływany w trakcie wywołania zwrotnego
    }

    public Node2D GetPlayerNode(string path, Vector2 position)
    {
        var playerScene = GD.Load<PackedScene>(path);
        var playerNode = playerScene.Instantiate() as Node2D;
        playerNode.Position = new Vector2(position.X, position.Y);
        playerNode.Name = "Player";
        return playerNode;
    }

    public Node2D GetPlayerCameraNode()
    {
        var playerCameraScene = GD.Load<PackedScene>("res://Scenes/Player/PlayerRunCamera.tscn");
        return playerCameraScene.Instantiate() as Node2D;
    }

    public EnemyNode GetEnemy(Vector2 position)
    {
        // TODO: Przerobić potem
        var enemyScene = GD.Load<PackedScene>("res://Scenes/Characters/Enemies/Goblin.tscn");
        var enemyInstance = enemyScene.Instantiate<EnemyNode>();
        enemyInstance.Init(
            new(
                name: "Goblin",
                maxHp: 30,
                attack: 10,
                defense: 10,
                critChance: 1,
                level: _service.PlayerCharacter.Level + 1,
                enemyType: EnemyType.Normal,
                logger: _logger,
                signals: _signals
            )
        );

        enemyInstance.Position = position;

        enemyInstance.GetNode<Label>("EnemyNameLabel").Text =
            $"{enemyInstance.Stats.Name} lvl: {enemyInstance.Stats.Level}";

        return enemyInstance;
    }
}
