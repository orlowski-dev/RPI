namespace Game.Core.Data;

/// <summary>
/// Baza nagród przypisana do nazw przeciwników.
/// Kluczem jest nazwa wroga, np. "goblin".
/// </summary>
public class EnemyRewardsDB
{
    public Dictionary<string, EnemyReward> EnemyRewards { get; set; }

    public EnemyRewardsDB()
    {
        EnemyRewards = new()
        {
            { "goblin", new EnemyReward(10) },

        };
    }
}
