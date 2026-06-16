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
        ActorStats stats,
        EnemyRank rank,
        EnemyType type,
        string? id = null,
        int? level = null
    )
        : base(stats: stats, level: level, id: id)
    {
        ExpReward = expReward;
        GoldReward = goldReward;
        Rank = rank;
        Type = type;
    }
}
