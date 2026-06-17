using Game.Core.Infrastructure.Save.Contracts;

namespace Game.Core.Infrastructure.Save.Snapshots;

public class PlayerSnapshot : ISnapshot
{
    public Guid Id { get; }
    public string Name { get; }
    public PlayerType Type { get; }
    public ActorStats Stats { get; }

    public PlayerSnapshot(Guid playerId, string name, PlayerType type, ActorStats stats)
    {
        Id = playerId;
        Name = name;
        Type = type;
        Stats = stats;
    }
}
