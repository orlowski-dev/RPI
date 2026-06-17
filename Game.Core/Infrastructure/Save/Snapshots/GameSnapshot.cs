using Game.Core.Domain.Session;

namespace Game.Core.Infrastructure.Save.Snapshots;

// główny
public record GameSnapshot(
    Guid Id,
    GameSessionState State,
    PlayerSnapshot Player,
    DungeonSnapshot? Dungeon
);
