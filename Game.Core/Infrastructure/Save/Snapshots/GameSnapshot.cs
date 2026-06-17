namespace Game.Core.Infrastructure.Save.Snapshots;

// stan świata - głównej sesji
public class GameSnapshot
{
    public Guid SessionId { get; }
    public PlayerSnapshot Player { get; }
    public DateTime CreatedAt { get; }

    public GameSnapshot(Guid sessionId, PlayerSnapshot player)
    {
        SessionId = sessionId;
        Player = player;
        CreatedAt = DateTime.Now;
    }
}
