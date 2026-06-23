public class ItemFactory
{
    private Random _random;

    public ItemFactory()
    {
        _random = new Random();
    }

    public Item Generate(string id, int playerLevel)
    {
        CatalogItemValues? catalogItemValues = null;
        if (!ItemCatalog.Values.TryGetValue(id, out catalogItemValues))
        {
            DebugExtension.Fatal(this, $"Item with id {id} not found.");
        }

        var rarity = RollRarity();

        return new(
            name: catalogItemValues.Name,
            category: catalogItemValues.Category,
            baseStats: catalogItemValues.BaseStats,
            level: _random.Next(playerLevel - 2, playerLevel + 3),
            rarity: rarity,
            allowedClasses: catalogItemValues.AllowedClasses
        );
    }

    private ItemRarity RollRarity()
    {
        var roll = _random.Next(1, 101);
        return roll switch
        {
            <= 60 => ItemRarity.Common,
            <= 85 => ItemRarity.Rare,
            <= 95 => ItemRarity.Epic,
            _ => ItemRarity.Legendary,
        };
    }
}
