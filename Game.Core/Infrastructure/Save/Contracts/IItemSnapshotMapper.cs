public interface IItemSnapshotMapper
{
    ItemSnapshot ToSnapshot(Item item);
    Item Restore(ItemSnapshot itemSnapshot);
}
