public class DungeonMapper : IDungeonSnapshotMapper
{
    private EncounterMapper _encounter;

    public DungeonMapper()
    {
        _encounter = new EncounterMapper();
    }

    public DungeonSnapshot? ToSnapshot(Dungeon? dungeon)
    {
        if (dungeon is null)
        {
            return null;
        }
        return new(
            Id: dungeon.Id,
            Encounters: dungeon.Encounters.Select((e) => _encounter.ToSnapshot(e)).ToList()
        );
    }

    public Dungeon? Restore(DungeonSnapshot? dungeonSnapshot)
    {
        if (dungeonSnapshot is null)
            return null;
        return new(
            id: dungeonSnapshot.Id,
            encounters: dungeonSnapshot.Encounters.Select((e) => _encounter.Restore(e)).ToList()
        );
    }
}
