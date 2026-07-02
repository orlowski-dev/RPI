public class EnemyMapper : IEnemySnapshotMapper
{
    public EnemySnapshot ToSnapshot(Enemy enemy)
    {
        return new(
            Id: enemy.Id,
            Name: enemy.Name,
            Type: enemy.Type,
            Stats: enemy.Stats,
            Level: enemy.Level,
            Rank: enemy.Rank,
            ExpReward: enemy.ExpReward,
            GoldReward: enemy.GoldReward,
            SubType: enemy.SubType
        );
    }

    public Enemy Restore(EnemySnapshot enemySnapshot)
    {
        return new(
            name: enemySnapshot.Name,
            stats: enemySnapshot.Stats,
            id: enemySnapshot.Id,
            level: enemySnapshot.Level,
            type: enemySnapshot.Type,
            rank: enemySnapshot.Rank,
            goldReward: enemySnapshot.GoldReward,
            expReward: enemySnapshot.ExpReward,
            subType: enemySnapshot.SubType
        );
    }
}
