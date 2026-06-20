public record ItemSnapshot(
    Guid Id,
    string Name,
    ItemCategory Category,
    ItemRarity Rarity,
    int Level,
    ItemStatsSnapshot BaseStats,
    IReadOnlyList<PlayerType> AllowedClasses
);
