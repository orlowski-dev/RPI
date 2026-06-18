public class Item
{
    public Guid Id { get; }
    public string Name { get; }
    public ACItemDefinition Definition { get; }
    public ItemRarity Rarity { get; }
    public int Level { get; }
    public ItemStats Stats { get; }

    public Item(
        Guid id,
        string name,
        ACItemDefinition definition,
        ItemRarity rarity,
        int level,
        ItemStats stats
    )
    {
        Id = id;
        Name = name;
        Definition = definition;
        Rarity = rarity;
        Level = level;
        Stats = stats;
    }
}
