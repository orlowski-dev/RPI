using Game.Core.Domain.Exploration;

namespace Game.Core.Infrastructure.Save.Snapshots;

// co dzieje się w świecie gry
public record DungeonSnapshot(Guid Id, IReadOnlyList<Encounter> Encounters);
