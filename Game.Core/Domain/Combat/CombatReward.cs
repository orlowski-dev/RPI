namespace Game.Core.Domain.Combat;

/// <summary>
/// Reprezentuje wynik walki.
/// </summary>
public partial class CombatReward
{
    public int Experience { get; }
    public int Gold { get; }
    public IReadOnlyList<string> ItemInstanceIds { get; }

    public CombatReward(int experience, int gold, IEnumerable<string> items)
    {
        Experience = experience;
        Gold = gold;
        ItemInstanceIds = items.ToList();
    }
}
