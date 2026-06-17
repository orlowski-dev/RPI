using Game.Core.Infrastructure.Save.Contracts;

namespace Game.Core.Infrastructure.Save.Snapshots;

// stan świata - głównej sesji
public class GameSnapshot : ISnapshot
{
    public Guid SessionId { get; }
    public PlayerSnapshot Player { get; }
    public DateTime CreatedAt { get; }
    public DungeonSnapshot? Dungeon { get; }

    public GameSnapshot(Guid sessionId, PlayerSnapshot player, DungeonSnapshot? dungeon = null)
    {
        SessionId = sessionId;
        Player = player;
        Dungeon = dungeon;
        CreatedAt = DateTime.Now;
    }
}
