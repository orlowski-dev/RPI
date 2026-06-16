namespace Game.Core.Domain.Combat.Actions;

/// <summary>
/// Bazowa akcja możliwa do wykonania podczas pojedynczej tury.
/// </summary>
public abstract class CombatAction
{
    /// <summary>
    /// Wykonuje logikę akcji dla podanej sesji walki.
    /// </summary>
    public abstract Result Execute(CombatSession session);
}
