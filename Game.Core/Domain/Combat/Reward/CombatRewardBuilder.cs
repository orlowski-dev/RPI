namespace Game.Core.Domain.Combat.Reward;

public class CombatRewardBuilder
{
    public CombatReward Generate()
    {
        var exp = 0;
        var gold = 0;
        var items = new List<string>();

        return new(experience: exp, gold: gold, items: items);
    }
}
