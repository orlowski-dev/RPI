/// <summary>
/// Reprezentuje wynik walki.
/// </summary>
public class CombatReward
{
    public int Experience { get; }
    public int Gold { get; }
    public int ExpBonus { get; } // do wyświetlenia na UI - dodatkowe bonusy
    public int GoldBonus { get; } // do wyświetlenia na UI - dodatkowe bonusy
    public IReadOnlyList<Item> Items { get; }

    public CombatReward(
        int experience,
        int gold,
        int expBonus,
        int goldBonus,
        IReadOnlyList<Item> items
    )
    {
        Experience = experience;
        Gold = gold;
        ExpBonus = expBonus;
        GoldBonus = goldBonus;
        Items = items;
    }
}
