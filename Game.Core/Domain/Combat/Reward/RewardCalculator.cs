namespace Game.Core.Domain.Combat.Reward;

// Punkty doświadczenia są sumowane za każdego przeciwnika w danej walce. Liczone są za
// pomocą:
// 𝑒𝑥𝑝𝑝𝑟𝑧1 = 𝑝𝑟𝑧1𝐿𝑣𝑙 * 10 * 𝑚𝑛𝑜ż𝑛𝑖𝑘𝑇𝑦𝑝𝑃𝑟𝑧𝑒𝑐𝑖𝑤𝑛𝑖𝑘𝑎
// 𝑒𝑥𝑝𝑝𝑟𝑧2 = 𝑝𝑟𝑧2𝐿𝑣𝑙 * 10 * 𝑚𝑛𝑜ż𝑛𝑖𝑘𝑇𝑦𝑝𝑃𝑟𝑧𝑒𝑐𝑖𝑤𝑛𝑖𝑘𝑎
// 𝑒𝑥𝑝𝑆𝑢𝑚 = 𝑒𝑥𝑝𝑝𝑟𝑧1 + 𝑒𝑥𝑝𝑝𝑟𝑧2 + 𝑒𝑥𝑝𝑝𝑟𝑧𝑁
// Jeżeli przeciwników jest więcej niż dwóch to wtedy bonus +10% exp.
// Złoto za przeciwnika jest liczone:
// 𝑔𝑜𝑙𝑑 = 𝑝𝑟𝑧𝑒𝑐𝐿𝑣𝑙 * 5 * 𝑡𝑦𝑝𝑃𝑟𝑧𝑒𝑐𝑖𝑤𝑛𝑖𝑘𝑎
// Dodatkowy bonus +10% golda za dodatkowego przeciwnika w walce.

public class RewardCalculator : IRewardCalculator
{
    private static readonly IReadOnlyDictionary<EnemyRank, double> _multipliers =
        EnemyMultipliers.Values;

    public CombatReward Calculate(IReadOnlyCollection<Enemy> defeatedEnemies)
    {
        var exp = defeatedEnemies.Sum(CalculateExp);
        var gold = defeatedEnemies.Sum(CalculateGold);
        int goldBonus = 0;
        int expBonus = 0;

        if (defeatedEnemies.Count > 2)
        {
            expBonus = (int)Math.Floor(exp * 0.1);
            exp += expBonus;
            goldBonus = (int)Math.Floor(gold * 0.1);
            gold += goldBonus;
        }

        return new(
            experience: exp,
            gold: gold,
            goldBonus: goldBonus,
            expBonus: expBonus,
            items: []
        );
    }

    private int CalculateExp(Enemy enemy)
    {
        double exp = enemy.ExpReward;
        exp = exp * enemy.Level * 10 * _multipliers[enemy.Rank];
        exp = Math.Floor(exp);

        return (int)exp;
    }

    private int CalculateGold(Enemy enemy)
    {
        double gold = enemy.GoldReward;
        gold = gold * enemy.Level * 5 * _multipliers[enemy.Rank];

        return (int)Math.Floor(gold);
    }
}
