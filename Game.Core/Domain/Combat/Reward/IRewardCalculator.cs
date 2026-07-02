public interface IRewardCalculator
{
    public CombatReward Calculate(IReadOnlyCollection<Enemy> defeatedEnemies, int playerLevel);
}
