using Godot;

[GlobalClass]
public partial class GameController : Node
{
    public static GameController Instance { get; private set; }
    public PlayerCharacter PlayerCharacter { get; private set; }
    public GameState GameState { get; private set; }
    private Signals _signals => Signals.Instance;

    public override void _EnterTree()
    {
        if (Instance != null)
        {
            GD.PushError("GameController już istnieje!");
            QueueFree();
            return;
        }

        Instance = this;
    }

    public override void _Ready()
    {
        GameState = GameState.MainMenu;
        _signals.SetGameState += OnSetGameState;
    }

    public override void _ExitTree()
    {
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

    private void OnSetGameState(GameState newState)
    {
        GameState = newState;
        GD.Print($"Game state changed to: {GameState}");
    }
}
