public class EnemyFactory
{
    private static readonly Random _random = new Random();

    /// <summary>
    /// zwracam randomowego enemy z definicji
    /// </summary>
    public Enemy Create()
    {
        var subType = GetRandomSubType();
        var def = EnemyDefinitions.Values[subType].Enemy;
        return def;
    }

    private EnemySubType GetRandomSubType()
    {
        var values = Enum.GetNames(typeof(EnemySubType));
        return (EnemySubType)_random.Next(0, values.Length);
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
