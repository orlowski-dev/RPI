using Game.Core.Domain.Exploration;
using Game.Core.Infrastructure.Save.Contracts;
using Game.Core.Infrastructure.Save.Snapshots;

namespace Game.Core.Infrastructure.Save.Mapping;

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
