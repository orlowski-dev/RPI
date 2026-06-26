public sealed class GameSessionProvider : IGameSessionProvider
{
    public GameSession? Current { get; private set; }
    public bool HasSession => Current is not null;

    public void Set(GameSession session)
    {
        Current = session;
    }

    public void Clear()
    {
        Current = null;
    }
}
