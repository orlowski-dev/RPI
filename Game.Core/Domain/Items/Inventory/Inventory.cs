public class Inventory
{
    public Backpack Backpack { get; }
    public Equipment Equipment { get; }

    public Inventory()
    {
        Backpack = new Backpack();
        Equipment = new Equipment();
    }

    public Inventory(Backpack backpack, Equipment equipment)
    {
        Backpack = backpack;
        Equipment = equipment;
    }
}
