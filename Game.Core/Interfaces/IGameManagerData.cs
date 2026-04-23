public interface IGameManagerData
{
    public PlayerCharacter PlayerCharacter { get; init; }
    public GameState GameState { get; init; }
    public EnemyCharacter EnemyCharacter { get; init; } // TODO: Potem zmienić na tablicę
}
