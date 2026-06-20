public interface IInventorySnapshotMapper
{
    InventorySnapshot ToSnapshot(Inventory inventory);
    Inventory Restore(InventorySnapshot snapshot);
}
