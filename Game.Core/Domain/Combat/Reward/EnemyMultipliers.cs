public static class EnemyMultipliers
{
    public static Dictionary<EnemyRank, double> Values { get; } =
        new()
        {
            [EnemyRank.Normal] = 1.0,
            [EnemyRank.Elite] = 1.5,
            [EnemyRank.Champion] = 2,
            [EnemyRank.Boss] = new Random().Next(2, 4),
        };
}
