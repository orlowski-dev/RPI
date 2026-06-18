public class Backpack
{
    public int Capacity { get; }
    public IReadOnlyList<Item> Items { get; private set; }

    public Backpack(int capacity)
    {
        Capacity = capacity;
        Items = new List<Item>();
    }

    public Backpack(int capacity, IReadOnlyList<Item> items)
    {
        Capacity = capacity;
        Items = items;
    }
}
