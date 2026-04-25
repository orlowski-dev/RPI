using Godot;

/// <summary>
/// Obiekt danych wykorzystywany do przekazywania aktualnego stanu gry.
/// </summary>
public partial class GameManagerData : GodotObject, IGameManagerData
{
    public GameState GameState { get; init; }
    public PlayerCharacter PlayerCharacter { get; init; }
    public EnemyCharacter EnemyCharacter { get; init; } // TODO: Potem zmienić na tablicę

    public GameManagerData(
        GameState gameState,
        PlayerCharacter playerCharacter = null,
        EnemyCharacter enemyCharacter = null
    )
    {
        GameState = gameState;
        PlayerCharacter = playerCharacter;
        EnemyCharacter = enemyCharacter;
    }
}
