public static class ItemStatCalculator
{
    public static ItemStats Calculate(ItemStats baseStats, ItemRarity rarity, int level)
    {
        var rarityMulti = ItemRarityMap.Values[rarity];
        var levelMulti = 1f + level * 0.1f;

        return new(
            (int)(baseStats.MaxHp * rarityMulti * levelMulti),
            (int)(baseStats.Attack * rarityMulti * levelMulti),
            (int)(baseStats.Defense * rarityMulti * levelMulti),
            (int)(baseStats.CriticalChance * rarityMulti),
            (int)(baseStats.Luck * rarityMulti)
        );
    }
}
