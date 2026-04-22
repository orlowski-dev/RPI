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
}
