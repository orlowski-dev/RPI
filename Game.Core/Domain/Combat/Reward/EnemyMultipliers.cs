public static class EnemyMultipliers
{
    public static Dictionary<EnemyRank, double> Values { get; } =
        new()
        {
            [EnemyRank.Normal] = 1.0,
            [EnemyRank.Elite] = 1.2f,
            [EnemyRank.Champion] = 1.5f,
            [EnemyRank.Boss] = Random.Shared.Next(2, 3),
        };
}
