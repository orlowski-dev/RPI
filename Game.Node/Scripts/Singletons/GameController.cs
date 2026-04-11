using Godot;
using Godot.Collections;

/// <summary>
/// Główny kontroler gry.
/// </summary>
/// <remarks>
/// Singleton zarządzający logiką gry. Zapewnia globalny dostęp do kontrolera gry.
/// </remarks>
[GlobalClass]
public partial class GameController : BaseSingleton<GameController>
{
    public PlayerCharacter PlayerCharacter { get; private set; }
    public GameState GameState { get; private set; }
    private Signals _signals => Signals.Instance;
    private Logger _logger => Logger.Instance;
    private Dictionary<string, string> _scenesMap = new()
    {
        { "testWorld", "res://Scenes/Testing/TestWorld.tscn" },
    };

    public override void _EnterTree()
    {
        base._EnterTree();
    }

    public override void _Ready()
    {
        base._Ready();
        GameState = GameState.MainMenu;
        _signals.SetGameState += OnSetGameState;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        _signals.SetGameState -= OnSetGameState;
    }

    public void StartNewGame()
    {
        PlayerCharacter = new(
            name: "Test Character",
            maxHp: 100,
            attack: 50,
            defense: 30,
            luck: 10,
            critChance: 1,
            characterClass: new(name: "Warrior", hpBonus: 20, attackBonus: 3, defenseBonus: 3),
            signals: _signals,
            logger: _logger
        );
    }

    /// <summary>
    /// Obsługuje zmianę stanu gry.
    /// </summary>
    /// <param name="newState">Nowy stan</param>
    private void OnSetGameState(GameState newState)
    {
        GameState = newState;
        GD.Print($"Game state changed to: {GameState}");

        switch (GameState)
        {
            case GameState.TestingPlayerMovement:
                GetTree().ChangeSceneToFile(_scenesMap["testWorld"]);
                return;
            default:
                GD.PrintErr("Invalid GameState value!");
                return;
        }
    }
}
