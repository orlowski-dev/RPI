public abstract class ACItemDefinition
{
    public readonly string Id;
    public string Name;
    public ItemCategory Category;
    public ItemStats BaseStats;
    public IReadOnlyList<PlayerType> AllowedClasses;

    protected ACItemDefinition(
        string id,
        string name,
        ItemCategory category,
        ItemStats baseStats,
        IReadOnlyList<PlayerType> allowedClasses
    )
    {
        Id = id;
        Name = name;
        Category = category;
        BaseStats = baseStats;
        AllowedClasses = allowedClasses;
    }
}
