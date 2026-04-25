using Godot;

public partial class MainMenu : Node
{
    [Export]
    public Button NewGameButton { get; set; }

    private Signals Signals => Signals.Instance;

    public override void _Ready()
    {
        NewGameButton.Pressed += StartNewGame;
    }

    private void StartNewGame()
    {
        Signals.EmitGameStateChanged(new GameManagerData(gameState: GameState.CharacterCreator));
    }
}
