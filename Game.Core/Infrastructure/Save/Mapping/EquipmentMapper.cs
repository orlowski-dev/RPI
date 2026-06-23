public class EquipmentMapper : IEquipmentSnapshotMapper
{
    private IItemSnapshotMapper _item;

    public EquipmentMapper()
    {
        _item = new ItemMapper();
    }

    public EquipmentSnapshot ToSnapshot(Equipment equipment)
    {
        return new(
            Weapon: equipment.Weapon is not null ? _item.ToSnapshot(equipment.Weapon) : null,
            Armor: equipment.Armor is not null ? _item.ToSnapshot(equipment.Armor) : null
        );
    }

    public Equipment Restore(EquipmentSnapshot snapshot)
    {
        return new(
            weapon: snapshot.Weapon is not null ? _item.Restore(snapshot.Weapon) : null,
            armor: snapshot.Armor is not null ? _item.Restore(snapshot.Armor) : null
        );
    }
}
