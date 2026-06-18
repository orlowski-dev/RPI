public static class EnemyMultipliers
{
    public static Dictionary<EnemyRank, double> Values { get; } =
        new()
        {
            [EnemyRank.Normal] = 1.0,
            [EnemyRank.Elite] = 2.5,
            [EnemyRank.Boss] = new Random().Next(5, 11),
        };
}
