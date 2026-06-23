public class InventoryMapper : IInventorySnapshotMapper
{
    private IEquipmentSnapshotMapper _eq;
    private IBackpackSnapshotMapper _backpack;

    public InventoryMapper()
    {
        _eq = new EquipmentMapper();
        _backpack = new BackpackMapper();
    }

    public InventorySnapshot ToSnapshot(Inventory inventory)
    {
        return new(
            Backpack: _backpack.ToSnapshot(inventory.Backpack),
            Equipment: _eq.ToSnapshot(inventory.Equipment)
        );
    }

    public Inventory Restore(InventorySnapshot snapshot)
    {
        return new();
    }
}
