public partial class EnemyCharacter : BaseCharacter
{
    public EnemyType EnemyType { get; private set; }
    public EnemyReward Reward { get; private set; }

    public EnemyCharacter(
        string name,
        int maxHp,
        int attack,
        int defense,
        int critChance,
        EnemyType enemyType,
        int level,
        EnemyReward reward,
        ISignals? signals = null,
        ILogger? logger = null
    )
        : base(name, maxHp, attack, defense, critChance, level, signals, logger)
    {
        EnemyType = enemyType;
    }
}
