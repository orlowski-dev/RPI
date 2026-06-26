public interface IGameSessionProvider
{
    GameSession? Current { get; }
    bool HasSession { get; }

    void Set(GameSession session);

    void Clear();
}
