public class ItemMapper : IItemSnapshotMapper
{
    private IItemStatsSnapshotMapper _stats;

    public ItemMapper()
    {
        _stats = new ItemStatsMapper();
    }

    public ItemSnapshot ToSnapshot(Item item)
    {
        return new(
            Id: item.Id,
            Name: item.Name,
            Category: item.Category,
            Rarity: item.Rarity,
            Level: item.Level,
            BaseStats: _stats.ToSnapshot(item.BaseStats),
            AllowedClasses: item.AllowedClasses
        );
    }

    public Item Restore(ItemSnapshot itemSnapshot)
    {
        return new(
            id: itemSnapshot.Id,
            name: itemSnapshot.Name,
            rarity: itemSnapshot.Rarity,
            category: itemSnapshot.Category,
            baseStats: _stats.Restore(itemSnapshot.BaseStats),
            allowedClasses: itemSnapshot.AllowedClasses,
            level: itemSnapshot.Level
        );
    }
}
