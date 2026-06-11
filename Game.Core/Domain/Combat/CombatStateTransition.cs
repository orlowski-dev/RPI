namespace Game.Core.Domain.Combat;

/// <summary>
/// Wynik aktualizacji stanu.
/// Określa czy należy przejść do kolejnego stanu.
/// </summary>
public record CombatStateTransition(bool ShouldChange, CombatStateType? NextState)
{
    public static void Stay()
    {
        return;
    }

    public static CombatStateTransition Next(CombatStateType next)
    {
        return new(true, next);
    }
}
