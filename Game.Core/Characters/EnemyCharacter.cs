namespace Game.Data
{
public enum EnemyType
{
    Zombie,
    Biegacz,
    Boss,
    Zboj,
    Brutal
}

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
        ISignals? signals = null,
        ILogger? logger = null
    )
        : base(name, maxHp, attack, defense, critChance, level, signals, logger)
    {
        EnemyType = enemyType;
    }

    public static Dictionary<EnemyType, EnemyCharacter> EnemyTypes = new()
    {
        {
            EnemyType.Zombie,
            new EnemyCharacter(
                name: "Zombie",
                maxHp: 90,
                attack: 10,
                defense: 4,
                critChance: 3,
                enemyType: EnemyType.Zombie,
                level: 1
            )
        },

        {
            EnemyType.Biegacz,
            new EnemyCharacter(
                name: "Biegacz",
                maxHp: 70,
                attack: 15,
                defense: 3,
                critChance: 10,
                enemyType: EnemyType.Biegacz,
                level: 1
            )
        },

        {
            EnemyType.Zboj,
            new EnemyCharacter(
                name: "Zbój",
                maxHp: 110,
                attack: 18,
                defense: 7,
                critChance: 12,
                enemyType: EnemyType.Zboj,
                level: 1
            )
        },

        {
            EnemyType.Brutal,
            new EnemyCharacter(
                name: "Brutal",
                maxHp: 160,
                attack: 22,
                defense: 14,
                critChance: 5,
                enemyType: EnemyType.Brutal,
                level: 1
            )
        },

        {
            EnemyType.Boss,
            new EnemyCharacter(
                name: "Boss",
                maxHp: 300,
                attack: 35,
                defense: 20,
                critChance: 20,
                enemyType: EnemyType.Boss,
                level: 1
            )
        }
    };
}
}
