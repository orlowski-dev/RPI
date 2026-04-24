public class EnemyRewardsDB
{
    public Dictionary<EnemyType, EnemyReward> EnemyRewards { get; set; }

    public EnemyRewardsDB()
    {
        EnemyRewards = new()
        {
            {
                EnemyType.Goblin,
                new(
                    gold: 10
                )
            },

        };
    }
}
