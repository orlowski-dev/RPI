public class Player : Actor
{
    public PlayerType Type { get; private set; }
    public int Exp { get; private set; }
    public int ExpNextLevel { get; private set; }
    public int Gold { get; private set; }

    private Inventory? _inventory;

    // public new ActorStats Stats => GetStats(_inventory);
    public override ActorStats Stats => GetStats(_inventory);

    public Player(
        string name,
        ActorStats stats,
        PlayerType type,
        Guid? id = null,
        int? level = null,
        Inventory? inventory = null
    )
        : base(name: name, stats: stats, id: id, level: level)
    {
        Type = type;
        Exp = 0;
        ExpNextLevel = CalculateExpNextLevel();
        Gold = 100;
        _inventory = inventory;
    }

    public void AddExperience(int amount)
    {
        if (amount <= 0)
            return;

        Exp += amount;

        while (Exp >= ExpNextLevel)
        {
            Exp -= ExpNextLevel;
            LevelUp();
            ExpNextLevel = CalculateExpNextLevel();
        }
    }

    public void AddGold(int amount)
    {
        if (amount <= 0)
            return;

        Gold += amount;
    }

    private int CalculateExpNextLevel()
    {
        return (int)Math.Floor(100 * Math.Pow(Level, 1.5));
    }

    protected override ActorStats RecalculateStats()
    {
        var map = PlayerProgressionMap.Values;

        return new(
            maxHp: Stats.MaxHp + Level + map[Type].MaxHp,
            attack: Stats.Attack + Level + map[Type].Attack,
            defense: Stats.Defense + Level + map[Type].Defense,
            criticalChance: Stats.CriticalChance,
            luck: Stats.Luck
        );
    }

    protected override ActorStats GetStats(Inventory? inventory = null)
    {
        if (inventory is null)
        {
            return base.Stats;
        }
        var maxHp = base.Stats.MaxHp;
        var currentHp = base.Stats.CurrentHp;
        var attack = base.Stats.Attack;
        var defense = base.Stats.Defense;
        var criticalChance = base.Stats.CriticalChance;
        var luck = base.Stats.Luck;

        Item? armor = inventory.Equipment.Armor;
        if (armor is not null)
        {
            maxHp += armor.BaseStats.MaxHp;
            attack += armor.BaseStats.Attack;
            defense += armor.BaseStats.Defense;
            luck += armor.BaseStats.Luck;
            criticalChance += armor.BaseStats.CriticalChance;
        }
        Item? weapon = inventory.Equipment.Weapon;
        if (weapon is not null)
        {
            maxHp += weapon.BaseStats.MaxHp;
            attack += weapon.BaseStats.Attack;
            defense += weapon.BaseStats.Defense;
            luck += weapon.BaseStats.Luck;
            criticalChance += weapon.BaseStats.CriticalChance;
        }

        return new(
            maxHp: maxHp,
            attack: attack,
            defense: defense,
            criticalChance: criticalChance,
            luck: luck,
            currentHp: currentHp
        );
    }

    public string TypePlural => PlayerDefinitions.Values[Type].TypePlural;

    public override string NodePath => PlayerDefinitions.Values[Type].NodePath;

    public override string DisplayName => $"{Name} (lvl: {Level})";

    public override string Info =>
        base.Info
        + $"Exp: {Exp}/{ExpNextLevel}\n"
        + $"HP: {Stats.CurrentHp}/{Stats.MaxHp}\n"
        + $"Gold: {Gold}\n"
        + $"Atak: {Stats.Attack}\n"
        + $"Obrona: {Stats.Defense}\n"
        + $"Szansa na\ntrafienie krytyczne: {Stats.Defense}%\n"
        + $"Szczęście: {Stats.Luck}%\n";
}
