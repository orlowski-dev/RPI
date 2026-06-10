namespace Game.Core.Domain.Combat;

// to jest snapshot obiektu np player bo nie powinno się od razu niszczyć głównej encji.

/// <summary>
/// Reprezentacja jednostki uczestniczącej w walce.
/// Stanowi adapter domenowy do systemu walki.
/// </summary>
public class CombatParticipant
{
    public string Id { get; }
    public CombatParticipantType Type { get; }
    public string SourceId { get; }
    public int CurrentHp { get; private set; }
    public CombatStats Stats { get; }
    public bool IsAlive => CurrentHp > 0;

    public CombatParticipant(
        string id,
        CombatParticipantType type,
        string sourceId,
        CombatStats stats
    )
    {
        Id = id;
        Type = type;
        SourceId = sourceId;
        Stats = stats;
        CurrentHp = stats.MaxHp;
    }

    public void ReceiveDamage(int value)
    {
        CurrentHp = Math.Max(0, CurrentHp - value);
    }

    public void Heal(int value)
    {
        CurrentHp = Math.Min(Stats.MaxHp, CurrentHp + value);
    }
}
