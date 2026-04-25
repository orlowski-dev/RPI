public partial class GameService
{
    public PlayerCharacter? PlayerCharacter { get; set; }
    public EnemyCharacter? EnemyCharacter { get; set; }
    public GameState GameState { get; set; }
    private Dictionary<string, string> _scenesMap;
    private readonly ISignals? _signals;
    private readonly ILogger? _logger;
    private string _scriptName;

    public GameService(ILogger? logger = null, ISignals? signals = null)
    {
        PlayerCharacter = null;
        EnemyCharacter = null;
        GameState = GameState.MainMenu;
        _scenesMap = new()
        {
            { "testWorld", "res://Scenes/Testing/TestWorld.tscn" },
            { "characterCreator", "res://Scenes/Levels/CharacterCreator.tscn" },
            { "cityScene", "res://Scenes/Levels/City/CityScene.tscn" },
            { "dungeonScene", "res://Scenes/Levels/Dungeon/DungeonScene.tscn" },
            { "combatScene", "res://Scenes/Levels/CombatScene.tscn" },
        };
        _logger = logger;
        _signals = signals;
        _scriptName = this.GetType().Name;
    }

    /// <summary>
    /// Zwraca ścieżkę do sceny w zależności od stanu rozgrywki.
    /// </summary>
    public string GetScenePath(IGameManagerData data)
    {
        switch (data.GameState)
        {
            case GameState.City:
                if (data.PlayerCharacter == null)
                {
                    _logger?.Write(LogLevel.Error, _scriptName, "PlayerCharacter jest null!");
                    throw new Exception("PlayerCharacter jest null!");
                }
                PlayerCharacter = data.PlayerCharacter;
                return _scenesMap["cityScene"];
            case GameState.CharacterCreator:
                return _scenesMap["characterCreator"];
            case GameState.Dungeon:
                return _scenesMap["dungeonScene"];
            case GameState.Combat:
                if (data.EnemyCharacter == null)
                {
                    _logger?.Write(LogLevel.Error, _scriptName, "EnemyCharacter jest null!");
                    throw new Exception("EnemyCharacter jest null!");
                }

                EnemyCharacter = data.EnemyCharacter;
                return _scenesMap["combatScene"];
            default:
                _logger?.Write(LogLevel.Error, _scriptName, "Nieobsługiwany GameState!");
                throw new Exception("Nieobsługiwany GameState!");
        }
    }
}
