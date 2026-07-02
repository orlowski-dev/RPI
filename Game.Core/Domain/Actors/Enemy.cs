public class Enemy : Actor
{
    public int ExpReward { get; init; }
    public int GoldReward { get; init; }
    public EnemyRank Rank { get; init; }
    public EnemyType Type { get; init; }
    public EnemySubType SubType { get; init; }

    public override ActorStats Stats => GetStats();

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

    public override string NodePath => EnemyDefinitions.Values[SubType].NodePath;
    public override string DisplayName => $"{Name}\n{new string('*', (int)Rank + 1)}";
}
