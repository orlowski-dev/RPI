public class Enemy : Actor
{
    public int ExpReward { get; private set; }
    public int GoldReward { get; private set; }
    public EnemyRank Rank { get; private set; }
    public EnemyType Type { get; private set; }

    public new ActorStats Stats => GetStats();

    public Enemy(
        string name,
        int expReward,
        int goldReward,
        ActorStats stats,
        EnemyRank rank,
        EnemyType type,
        Guid? id = null,
        int? level = null
    )
        : base(name: name, stats: stats, level: level, id: id)
    {
        ExpReward = expReward;
        GoldReward = goldReward;
        Rank = rank;
        Type = type;
    }
}
