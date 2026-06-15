namespace Game.Core.Domain.Combat.Reward;

public interface IRewardCalculator
{
    public CombatReward Calculate(IReadOnlyCollection<Enemy> defeatedEnemies);
}
