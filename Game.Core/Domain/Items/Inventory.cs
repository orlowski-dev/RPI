public class Inventory
{
    public Backpack Backpack { get; }
    public Equipment Equipment { get; }

    public Inventory(Backpack backpack, Equipment equipment)
    {
        Backpack = backpack;
        Equipment = equipment;
    }

    public Result Add(Item item)
    {
        throw new NotImplementedException();
    }

    public Result Equip(Guid itemId)
    {
        throw new NotImplementedException();
    }

    public Result Remove(Guid itemId)
    {
        throw new NotImplementedException();
    }
}
