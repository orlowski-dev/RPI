public class Backpack
{
    public int Capacity { get; private set; }
    public List<Item> Items { get; private set; }

    public Backpack()
    {
        Capacity = 20;
        Items = new List<Item>();
    }

    public void SetCapacity(int newValue)
    {
        Capacity = newValue;
    }

    public Result Add(Item item)
    {
        if (Items.Count >= Capacity)
        {
            return Result.Fail(new("Backpack is full", ErrorType.Validation));
        }
        Items.Add(item);
        return Result.Success();
    }

    public Result Remove(Guid itemId)
    {
        var found = Items.Find((i) => i.Id == itemId);
        if (found is null)
        {
            return Result.Fail(
                new($"Item {itemId} does not exist in the backpack!", ErrorType.NotFound)
            );
        }

        Items.Remove(found);
        return Result.Success();
    }
}
