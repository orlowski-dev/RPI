public record CatalogItemValues(
    string Name,
    ItemCategory Category,
    ItemStats BaseStats,
    IReadOnlyList<PlayerType> AllowedClasses
);
