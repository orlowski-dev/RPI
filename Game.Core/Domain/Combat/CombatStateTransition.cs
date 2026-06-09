namespace Game.Core.Domain.Combat;

/// <summary>
/// Wynik aktualizacji stanu.
/// Określa czy należy przejść do kolejnego stanu.
/// </summary>
public record CombatStateTransition(bool ShouldChange, CombatStateType? NextState)
{
    public static CombatStateTransition Stay()
    {
        return new(true, null);
    }

    public static CombatStateTransition Next(CombatStateType next)
    {
        return new(true, next);
    }
}
