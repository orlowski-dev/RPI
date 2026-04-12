using Godot;

public partial class MainMenu : Node
{
    [Export]
    public Button NewGameButton { get; set; }

    private Signals _signals => Signals.Instance;

    public override void _Ready()
    {
        NewGameButton.Pressed += StartNewGame;
    }

    private void StartNewGame()
    {
        _signals.EmitGameStateChanged(
            (IGameManagerData)
                new GameManagerData(
                    gameState: GameState.TestingPlayerMovement,
                    playerCharacter: new(
                        name: "Test Character",
                        maxHp: 100,
                        attack: 50,
                        defense: 30,
                        luck: 10,
                        critChance: 1,
                        characterClass: new(
                            name: "Warrior",
                            hpBonus: 20,
                            attackBonus: 3,
                            defenseBonus: 3
                        )
                    )
                )
        );
    }
}
