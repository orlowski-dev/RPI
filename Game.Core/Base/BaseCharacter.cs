/// <summary>
/// Struktra bazowa dla postaci gracza i przeciwników
/// </summary>
/// <param name="Name">Nazwa postaci</param>
/// <param name="HP">Punkty życia</param>
/// <param name="HP">Maksymalne punkty życia</param>
/// <param name="Attack">Siła ataku</param>
/// <param name="Defense">Obrona</param>
/// <param name="CritChance">Szansa na trafienie krytyczne (%)</param>
/// <param name="Level">Poziom postaci</param>
public abstract partial class BaseCharacter
{
    private readonly ISignals? _signals;
    private readonly ILogger? _logger;
    private int _hp;

    public string Name { get; private set; }
    public int MaxHP { get; private set; }
    public int HP
    {
        get => _hp;
        private set
        {
            _hp = value;
            _signals?.EmitCharacterHpChanged(_hp);
            _logger?.Write(
                LogLevel.Info,
                this.GetType().Name,
                "HP postaci zostało ustawione na: " + _hp
            );
        }
    }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int CritChance { get; private set; }
    public int Level { get; private set; }

    protected BaseCharacter(
        string name,
        int maxHp,
        int attack,
        int defense,
        int critChance,
        int level,
        ISignals? signals = null,
        ILogger? logger = null
    )
    {
        Name = name;
        MaxHP = maxHp;
        HP = MaxHP;
        Attack = attack;
        Defense = defense;
        CritChance = critChance;
        Level = level;
        _signals = signals;
        _logger = logger;
    }

    /// <summary>
    /// Zmniejsza HP postaci o podaną wartość.
    /// </summary>
    /// <param name="amount">Ilość obrażeń</param>
    public virtual void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
    }

    /// <summary>
    /// Leczy postać o podaną wartość.
    /// </summary>
    /// <param name="amount">Ilość leczenia</param>
    public virtual void Heal(int amount)
    {
        if (HP + amount > MaxHP)
        {
            HP = MaxHP;
        }
        else
        {
            HP += amount;
        }
    }

    // <summary>
    /// Setter dla Level
    /// </summary>
    /// <param name="newLvl">Nowa wartość (lvl)</param>
    protected void SetLevel(int newLvl)
    {
        Level = newLvl;
    }

    protected void ApplyLevelUpBonusses(CharacterClass characterClass)
    {
        HP += characterClass.HpBonus;
        Attack += characterClass.AttackBonus;
        Defense += characterClass.DefenseBonus;
    }
}
