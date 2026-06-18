public interface IDungeonSnapshotMapper
{
    DungeonSnapshot? ToSnapshot(Dungeon? dungeon);
    Dungeon? Restore(DungeonSnapshot? dungeonSnapshot);
}
