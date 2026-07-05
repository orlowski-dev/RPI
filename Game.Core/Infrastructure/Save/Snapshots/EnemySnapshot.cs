public record EnemySnapshot(
    Guid Id,
    string Name,
    EnemyType Type,
    EnemyRank Rank,
    ActorStats Stats,
    int Level,
    int ExpReward,
    int GoldReward,
    EnemySubType SubType
) : ARActorSnapshot(Id, Name, Stats, Level);
