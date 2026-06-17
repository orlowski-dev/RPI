using Game.Core.Domain.Exploration;

namespace Game.Core.Infrastructure.Save.Snapshots;

public class DungeonSnapshot
{
    public Guid Id;
    public IReadOnlyList<Encounter> Encounters;

    public DungeonSnapshot(Guid id, IReadOnlyList<Encounter> encounters)
    {
        Id = id;
        Encounters = encounters;
    }
}
