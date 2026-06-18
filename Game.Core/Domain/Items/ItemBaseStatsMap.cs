public static class ItemBaseStatsMap
{
    public static Dictionary<string, ItemStats> Values { get; } =
        new()
        {
            // Warrior
            ["iron_sword"] = new(MaxHp: 0, Attack: 12, Defense: 2, CriticalChance: 3, Luck: 0),
            ["demacia_axe"] = new(MaxHp: 0, Attack: 20, Defense: -3, CriticalChance: 8, Luck: 1),
            ["guardian_sword"] = new(MaxHp: 0, Attack: 14, Defense: 5, CriticalChance: 2, Luck: 0),
            // Archer
            ["hunter_bow"] = new(MaxHp: 0, Attack: 14, Defense: 1, CriticalChance: 10, Luck: 3),
            // Mage
            ["apprentice_staff"] = new(
                MaxHp: 0,
                Attack: 16,
                Defense: 1,
                CriticalChance: 8,
                Luck: 2
            ),
            // Armor
            ["leather_armor"] = new(MaxHp: 10, Attack: 0, Defense: 8, CriticalChance: 2, Luck: 1),
        };
}
