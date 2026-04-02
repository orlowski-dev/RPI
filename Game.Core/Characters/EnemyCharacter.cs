public partial class EnemyCharacter : BaseCharacter
{
    public EnemyType EnemyType { get; private set; }

    public EnemyCharacter(
        string name,
        int maxHp,
        int attack,
        int defense,
        int critChance,
        EnemyType enemyType,
        int level,
        ISignals? signals
    )
        : base(name, maxHp, attack, defense, critChance, level, signals)
    {
        EnemyType = enemyType;
    }
}
