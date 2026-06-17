namespace Game.Core.Infrastructure.Save.Snapshots;

public class PlayerSnapshot
{
    public Guid Id;
    public string Name;
    public PlayerType Type;
    public ActorStats Stats;
    public int Level;

    public PlayerSnapshot(Guid playerId, string name, PlayerType type, ActorStats stats, int level)
    {
        Id = playerId;
        Name = name;
        Type = type;
        Stats = stats;
        Level = level;
    }
}
