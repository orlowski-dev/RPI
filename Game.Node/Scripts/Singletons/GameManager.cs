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

    public PlayerCharacter GetPlayerCharacter()
    {
        return _service.PlayerCharacter;
    }

    private void OnGameStateChanged(GameManagerData data)
    {
        var scenePath = _service.GetScenePath(data);
        GetTree().ChangeSceneToFile(scenePath);
    }
}
