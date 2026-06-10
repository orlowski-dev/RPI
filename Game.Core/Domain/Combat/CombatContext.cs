namespace Game.Core.Domain.Combat;

/// <summary>
/// Obiekt kontekstowy przekazywany pomiędzy stanami walki.
/// Ogranicza liczbę parametrów przekazywanych pomiędzy komponentami domeny.
/// </summary>
public class CombatContext
{
    public CombatSession Session { get; }

    public CombatContext(CombatSession session)
    {
        Session = session;
    }
}
