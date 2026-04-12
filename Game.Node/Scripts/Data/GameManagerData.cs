using Godot;

public partial class GameManagerData : GodotObject, IGameManagerData
{
    public GameState GameState { get; init; }
    public PlayerCharacter PlayerCharacter { get; init; }

    public GameManagerData(GameState gameState, PlayerCharacter playerCharacter = null)
    {
        GameState = gameState;
        PlayerCharacter = playerCharacter;
    }
}
