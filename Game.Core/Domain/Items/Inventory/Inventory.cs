public class Inventory
{
    public Backpack Backpack { get; }
    public Equipment Equipment { get; }

    public Inventory(PlayerType playerType)
    {
        Backpack = new Backpack();
        Equipment = new Equipment(playerType: playerType);
    }
}
