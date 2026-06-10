namespace Game.Core.Domain.Combat;

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
    CombatStateType Type { get; }

    // wywoływane raz - wchodzę do stanu np. tura gracza się zaczęła
    void Enter(CombatContext ctx);

    // wywołuje się wiele razy - czy gracz JUŻ wykonał akcję?
    CombatStateTransition Update(CombatContext ctx);

    // wywoływane raz - sprzątanie po stanie - np. kończę turę
    void Exit(CombatContext ctx);
}
