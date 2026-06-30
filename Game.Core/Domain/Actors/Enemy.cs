public class Enemy : Actor
{
    public int ExpReward { get; init; }
    public int GoldReward { get; init; }
    public EnemyRank Rank { get; init; }
    public EnemyType Type { get; init; }
    public EnemySubType SubType { get; init; }

    public new ActorStats Stats => GetStats();

    public Enemy(
        string name,
        int expReward,
        int goldReward,
        ActorStats stats,
        EnemyRank rank,
        EnemyType type,
        EnemySubType subType,
        Guid? id = null,
        int? level = null
    )
        : base(name: name, stats: stats, level: level, id: id)
    {
        ExpReward = expReward;
        GoldReward = goldReward;
        Rank = rank;
        Type = type;
        SubType = subType;
    }

    public string NodePath => EnemyDefinitions.Values[SubType].NodePath;
}
