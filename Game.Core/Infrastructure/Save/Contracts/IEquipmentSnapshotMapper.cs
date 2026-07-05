public interface IEquipmentSnapshotMapper
{
    EquipmentSnapshot ToSnapshot(Equipment Equipment);
    Equipment Restore(EquipmentSnapshot snapshot);
}
