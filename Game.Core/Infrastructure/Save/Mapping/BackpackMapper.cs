public class BackpackMapper : IBackpackSnapshotMapper
{
    private IItemSnapshotMapper _item;

    public BackpackMapper()
    {
        _item = new ItemMapper();
    }

    public BackpackSnapshot ToSnapshot(Backpack backpack)
    {
        return new(
            Capacity: backpack.Capacity,
            Items: backpack.Items.Select((x) => _item.ToSnapshot(x)).ToList()
        );
    }

    public Backpack Restore(BackpackSnapshot snapshot)
    {
        return new(
            capacity: snapshot.Capacity,
            items: snapshot.Items.Select((x) => _item.Restore(x)).ToList()
        );
    }
}
