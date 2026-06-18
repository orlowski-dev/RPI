public class DungeonMapper : IDungeonSnapshotMapper
{
    public DungeonSnapshot? ToSnapshot(Dungeon? dungeon)
    {
        if (dungeon is null)
        {
            return null;
        }
        return new(Id: dungeon.Id, Encounters: dungeon.Encounters);
    }

    public Dungeon? Restore(DungeonSnapshot? dungeonSnapshot)
    {
        if (dungeonSnapshot is null)
            return null;
        return new(id: dungeonSnapshot.Id, encounters: dungeonSnapshot.Encounters);
    }
}
