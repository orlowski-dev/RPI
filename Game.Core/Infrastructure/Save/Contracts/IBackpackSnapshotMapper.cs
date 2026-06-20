public interface IBackpackSnapshotMapper
{
    BackpackSnapshot ToSnapshot(Backpack Backpack);
    Backpack Restore(BackpackSnapshot snapshot);
}
