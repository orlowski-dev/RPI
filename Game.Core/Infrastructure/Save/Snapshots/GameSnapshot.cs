namespace Game.Core.Infrastructure.Save.Snapshots;

// główny
public record GameSnapshot(PlayerSnapshot Player, DungeonSnapshot Dungeon);
