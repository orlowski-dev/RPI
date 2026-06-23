public static class ItemCatalog
{
    public static Dictionary<string, CatalogItemValues> Values { get; } =
        new()
        {
            ["iron_sword"] = new(
                Name: "Żelazny miecz",
                Category: ItemCategory.Weapon,
                BaseStats: new(0, 12, 2, 3, 0),
                AllowedClasses: [PlayerType.Warrior]
            ),
            ["demacia_axe"] = new(
                Name: "Miecz Demacjanina",
                Category: ItemCategory.Weapon,
                BaseStats: new(0, 20, -3, 8, 1),
                AllowedClasses: [PlayerType.Warrior]
            ),
            ["guardian_sword"] = new(
                Name: "Miecz Strażnika",
                Category: ItemCategory.Weapon,
                BaseStats: new(0, 14, 5, 2, 0),
                AllowedClasses: [PlayerType.Warrior]
            ),
            ["hunter_bow"] = new(
                Name: "Łuk myśliwego",
                Category: ItemCategory.Weapon,
                BaseStats: new(0, 14, 1, 10, 3),
                AllowedClasses: [PlayerType.Archer]
            ),
            ["wand"] = new(
                Name: "Różdżka",
                Category: ItemCategory.Weapon,
                BaseStats: new(0, 16, 1, 8, 2),
                AllowedClasses: [PlayerType.Archer]
            ),
            ["plate_armor"] = new(
                Name: "Pancerz płytkowy",
                Category: ItemCategory.Armor,
                BaseStats: new(15, 0, 16, 0, 0),
                AllowedClasses: [PlayerType.Archer, PlayerType.Mage, PlayerType.Warrior]
            ),
        };
}
