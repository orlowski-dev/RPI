public static class ItemRarityMap
{
    public static Dictionary<ItemRarity, float> Values { get; } =
        new()
        {
            [ItemRarity.Common] = 1.0f,
            [ItemRarity.Rare] = 1.5f,
            [ItemRarity.Epic] = 2.2f,
            [ItemRarity.Legendary] = 3.5f,
        };
}
