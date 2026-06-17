namespace Game.Core.Infrastructure.Save.Snapshots;

public record GameSnapshot(PlayerSnapshot Player, DungeonSnapshot Dungeon);
