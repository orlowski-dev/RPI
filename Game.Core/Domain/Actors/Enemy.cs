namespace Game.Core.Domain.Actors;

public class Enemy : Actor
{
    public int ExpReward { get; private set; }
    public int GoldReward { get; private set; }
    public EnemyRank Rank { get; private set; }
    public EnemyType Type { get; private set; }

    public Enemy(
        int expReward,
        int goldReward,
        string id,
        ActorBaseStats baseStats,
        EnemyRank rank,
        EnemyType type,
        int? level = null
    )
        : base(id: id, baseStats: baseStats, level: level)
    {
        ExpReward = expReward;
        GoldReward = goldReward;
        Rank = rank;
        Type = type;
    }
}
