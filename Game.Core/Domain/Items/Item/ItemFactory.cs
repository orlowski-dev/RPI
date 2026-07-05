public class ItemFactory
{
    private Random _random;

    public ItemFactory(Random random)
    {
        _random = random;
    }

    /// <summary>
    /// Generuje randomwą wariację przediotu z katalogu przedmiotów po id przedmiotu w katalogu.
    /// </summary>
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
            level: playerLevel == 1 ? 1 : _random.Next(playerLevel - 1, playerLevel + 3),
            rarity: rarity,
            allowedClasses: catalogItemValues.AllowedClasses
        );
    }

    /// <summary>
    /// Generuje randomwą wariację przediotu z katalogu przedmiotów.
    /// </summary>
    public Item GenerateRandom(int playerLevel)
    {
        var keys = ItemCatalog.Values.Keys.ToList();
        var randomId = keys[_random.Next(0, keys.Count)];
        return Generate(id: randomId, playerLevel: playerLevel);
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

    public Item GenerateStartItem(ItemCategory category, PlayerType playerType)
    {
        var candidates = ItemCatalog
            .Values.Where(kv =>
                kv.Value.Category == category && kv.Value.AllowedClasses.Contains(playerType)
            )
            .Select(kv => kv.Key)
            .ToList();

        if (candidates.Count == 0)
            throw new InvalidOperationException(
                $"Brak itemów w katalogu: {category} dla {playerType}."
            );

        var id = candidates[_random.Next(candidates.Count)];
        return Generate(id: id, playerLevel: 1);
    }
}
