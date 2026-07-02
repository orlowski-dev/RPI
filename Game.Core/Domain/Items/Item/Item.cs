public class Item
{
    public Guid Id { get; }
    public string Name { get; }
    public ItemCategory Category { get; }
    public ItemRarity Rarity { get; }
    public int Level { get; }
    public ItemStats BaseStats { get; }
    public IReadOnlyList<PlayerType> AllowedClasses { get; }

    public ItemStats Stats =>
        ItemStatCalculator.Calculate(baseStats: BaseStats, rarity: Rarity, level: Level);

    public string Prefix =>
        Rarity switch
        {
            ItemRarity.Rare => "Rzadki",
            ItemRarity.Epic => "Epicki",
            ItemRarity.Legendary => "Legendarny",
            _ => "",
        };

    public Item(
        string name,
        ItemCategory category,
        ItemRarity rarity,
        int level,
        ItemStats baseStats,
        IReadOnlyList<PlayerType> allowedClasses,
        Guid? id = null
    )
    {
        Id = id ?? Guid.NewGuid();
        Category = category;
        Rarity = rarity;
        Name = string.Join(' ', [Prefix, name]);
        Level = level;
        BaseStats = baseStats;
        AllowedClasses = allowedClasses;
    }

    public string GetInfo()
    {
        var msg = "";
        msg += $"{Name}\nRzadkość: {Prefix}\n";

        if (Stats.MaxHp > 0)
        {
            msg += $"+{Stats.MaxHp} HP\n";
        }

        if (Stats.Attack > 0)
        {
            msg += $"+{Stats.Attack} atak\n";
        }
        if (Stats.Defense > 0)
        {
            msg += $"+{Stats.Defense} obrona\n";
        }
        if (Stats.CriticalChance > 0)
        {
            msg += $"+{Stats.CriticalChance}% szansa na trafienie kryt.\n";
        }
        if (Stats.Luck > 0)
        {
            msg += $"+{Stats.Luck}% szczęście\n";
        }

        return msg;
    }
}
