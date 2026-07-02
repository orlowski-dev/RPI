public class EnemyFactory
{
    /// <summary>
    /// zwracam randomowego enemy z definicji
    /// </summary>
    public Enemy Create()
    {
        var subType = GetRandomSubType();
        var def = EnemyDefinitions.Values[subType].Enemy;
        return new Enemy(
            name: def.Name,
            expReward: def.ExpReward,
            goldReward: def.GoldReward,
            stats: new ActorStats(
                maxHp: def.Stats.MaxHp,
                attack: def.Stats.Attack,
                defense: def.Stats.Defense,
                criticalChance: def.Stats.CriticalChance,
                luck: def.Stats.Luck
            ),
            rank: def.Rank,
            type: def.Type,
            subType: def.SubType
        );
    }

    private EnemySubType GetRandomSubType()
    {
        var values = Enum.GetNames(typeof(EnemySubType));
        return (EnemySubType)Random.Shared.Next(0, values.Length);
    }

    public Enemy CreateNonBossEnemy()
    {
        Enemy enemy;
        do
        {
            enemy = Create();
        } while (enemy.Rank == EnemyRank.Boss);

        return enemy;
    }

    public Enemy CreateBossEnemy()
    {
        Enemy enemy;
        do
        {
            enemy = Create();
        } while (enemy.Rank != EnemyRank.Boss);

        return enemy;
    }
}
