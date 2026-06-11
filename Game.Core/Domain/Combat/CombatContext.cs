namespace Game.Core.Domain.Combat;

/// <summary>
/// Obiekt kontekstowy przekazywany pomiędzy stanami walki.
/// Udostępnia wspólne dane wymagane podczas wykonywania przebiegu walki.
/// </summary>
public class CombatContext
{
    public CombatSession Session { get; }

    public CombatContext(CombatSession session)
    {
        Session = session;
    }
}
