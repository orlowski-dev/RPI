namespace Game.Core.Domain.Combat.States;

/// <summary>
/// Definiuje pojedynczy stan walki.
///
/// Każdy stan:
/// - wykonuje inicjalizację,
/// - aktualizuje logikę,
/// - zwraca informację o przejściu.
/// </summary>
public interface ICombatState
{
    public CombatStateType Type { get; }

    public bool ReturnsControlToUi { get; } // true -> zwróc dto

    // wywoływane raz - wchodzę do stanu np. tura gracza się zaczęła
    public void Enter(CombatContext ctx);

    // wywołuje się wiele razy - czy gracz JUŻ wykonał akcję? co potem?
    public CombatStateTransition Update(CombatContext ctx);

    // wywoływane raz - sprzątanie po stanie - np. kończę turę
    public void Exit(CombatContext ctx);
}
