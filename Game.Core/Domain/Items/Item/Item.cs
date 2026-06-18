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

    public Item(
        string name,
        ItemCategory category,
        ItemRarity rarity,
        int level,
        ItemStats baseStats,
        IReadOnlyList<PlayerType> allowedClasses
    )
    {
        Id = Guid.NewGuid();
        Name = name;
        Category = category;
        Rarity = rarity;
        Level = level;
        BaseStats = baseStats;
        AllowedClasses = allowedClasses;
    }
}
