/// <summary>
/// Reprezentuje wynik walki.
/// </summary>
public class CombatReward
{
    public int Experience { get; }
    public int Gold { get; }
    public int ExpBonus { get; } // do wyświetlenia na UI
    public int GoldBonus { get; } // do wyświetlenia na UI
    public IReadOnlyList<string> ItemInstanceIds { get; }

    public CombatReward(
        int experience,
        int gold,
        int expBonus,
        int goldBonus,
        IEnumerable<string> items
    )
    {
        Experience = experience;
        Gold = gold;
        ExpBonus = expBonus;
        GoldBonus = goldBonus;
        ItemInstanceIds = items.ToList();
    }
}
