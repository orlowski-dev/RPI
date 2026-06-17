using Game.Core.Domain.Exploration;
using Game.Core.Infrastructure.Save.Contracts;

namespace Game.Core.Infrastructure.Save.Snapshots;

public class DungeonSnapshot : ISnapshot
{
    public Guid Id;
    public IReadOnlyList<Encounter> Encounters;

    public DungeonSnapshot(Guid id, IReadOnlyList<Encounter> encounters)
    {
        Id = id;
        Encounters = encounters;
    }
}
