namespace Game.Tests.Combat;

public class RewardTest
{
    private readonly RewardCalculator _rewardCalculator = new();

    private static Enemy CreateEnemy(
        int level,
        EnemyRank rank,
        EnemyType type,
        int expReward,
        int goldReward
    )
    {
        return new Enemy(
            expReward: expReward,
            goldReward: goldReward,
            id: "enemy",
            rank: rank,
            type: type,
            level: level,
            stats: new(10, 3, 1, 2, 3)
        );
    }

    [Fact]
    public void ShouldCalculateRewardForSingleNormalEnemy()
    {
        DebugExtension.Log(this, "Starting..");

        var enemy = new Enemy(
            goldReward: 1,
            expReward: 1,
            id: "enemy",
            level: 10,
            stats: new(10, 3, 1, 2, 3),
            rank: EnemyRank.Normal,
            type: EnemyType.Goblin
        );

        // exp = 1 * 10 * 10 * 1 = 100
        // gold = 1 * 10 * 5 * 1 = 50

        var result = _rewardCalculator.Calculate([enemy]);
        Assert.Equal(100, result.Experience);
        Assert.Equal(50, result.Gold);
    }

    [Fact]
    public void Should_sum_rewards_from_multiple_enemies()
    {
        DebugExtension.Log(this, "Starting..");

        var enemies = new[]
        {
            CreateEnemy(10, EnemyRank.Normal, EnemyType.Goblin, 1, 1),
            CreateEnemy(5, EnemyRank.Normal, EnemyType.Goblin, 1, 1),
        };

        // exp = 100 + 50
        // gold = 50 + 25

        var result = _rewardCalculator.Calculate(enemies);
        Assert.Equal(150, result.Experience);
        Assert.Equal(75, result.Gold);
    }

    [Fact]
    public void Should_add_10_percent_bonus_when_more_than_two_enemies()
    {
        DebugExtension.Log(this, "Starting..");
        var enemies = new[]
        {
            CreateEnemy(10, EnemyRank.Normal, EnemyType.Goblin, 1, 1),
            CreateEnemy(10, EnemyRank.Normal, EnemyType.Goblin, 1, 1),
            CreateEnemy(10, EnemyRank.Normal, EnemyType.Goblin, 1, 1),
        };

        // base exp = 300
        // bonus = 30
        // total = 330

        // base gold = 150
        // bonus = 15
        // total = 165

        var result = _rewardCalculator.Calculate(enemies);
        Assert.Equal(330, result.Experience);
        Assert.Equal(165, result.Gold);
    }

    [Fact]
    public void Should_use_rank_multiplier()
    {
        DebugExtension.Log(this, "Starting..");
        var normal = CreateEnemy(10, EnemyRank.Normal, EnemyType.Goblin, 1, 1);
        var elite = CreateEnemy(10, EnemyRank.Elite, EnemyType.Goblin, 1, 1);
        var normalReward = _rewardCalculator.Calculate([normal]);
        var eliteReward = _rewardCalculator.Calculate([elite]);
        Assert.True(eliteReward.Experience > normalReward.Experience);
        Assert.True(eliteReward.Gold > normalReward.Gold);
    }

    [Fact]
    public void Should_return_zero_when_no_enemies()
    {
        DebugExtension.Log(this, "Starting..");
        var result = _rewardCalculator.Calculate([]);
        Assert.Equal(0, result.Experience);
        Assert.Equal(0, result.Gold);
    }
}
