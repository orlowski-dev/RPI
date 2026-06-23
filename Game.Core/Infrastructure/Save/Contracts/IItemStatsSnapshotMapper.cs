public interface IItemStatsSnapshotMapper
{
    ItemStatsSnapshot ToSnapshot(ItemStats itemStats);
    ItemStats Restore(ItemStatsSnapshot snapshot);
}
