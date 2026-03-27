using Godot;

/// <summary>
/// Struktra bazowa dla postaci gracza i przeciwników
/// </summary>
/// <param name="Name">Nazwa postaci</param>
/// <param name="HP">Punkty życia</param>
/// <param name="Attack">Punkty życia</param>
/// <param name="Defense">Punkty życia</param>
/// <param name="Luck">Punkty życia</param>
/// <param name="CritChance">Szansa na trafienie krytyczne (%)</param>
public abstract partial class BaseCharacter : Resource
{
    [Signal]
    public delegate void HpChangedEventHandler(int hp);

    private int _hp;

    public string Name { get; private set; }
    public int MaxHP { get; private set; }
    public int HP
    {
        get => _hp;
        private set
        {
            _hp = value;
            EmitSignal(SignalName.HpChanged, _hp);
        }
    }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Luck { get; private set; }
    public int CritChance { get; private set; }

    protected BaseCharacter(
        string name,
        int maxHp,
        int attack,
        int defense,
        int luck,
        int critChance
    )
    {
        Name = name;
        MaxHP = maxHp;
        HP = MaxHP;
        Attack = attack;
        Defense = defense;
        Luck = luck;
        CritChance = critChance;
    }

    public virtual void TakeDamage(int amount)
    {
        HP = Mathf.Max(0, HP - amount);
    }

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
}
