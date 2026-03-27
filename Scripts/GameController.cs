using Godot;

[GlobalClass]
public partial class GameController : Node
{
    public static GameController Instance { get; private set; }
    public PlayerCharacter PlayerCharacter { get; private set; }

    public override void _EnterTree()
    {
        if (Instance != null)
        {
            GD.PushError("GameManager już istnieje!");
            QueueFree();
            return;
        }

        Instance = this;
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
}
