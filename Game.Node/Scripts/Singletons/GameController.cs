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
        GD.Print("starting new game..");
        PlayerCharacter = new(
            name: "Character name",
            maxHp: 100,
            attack: 50,
            defense: 10,
            luck: 10,
            critChance: 2
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
