namespace Game.Core.Domain.Combat.States;

public class PlayerStatusState : ICombatState
{
    public CombatStateType Type => CombatStateType.PlayerStatus;

    // public bool IsAutomatic { get; } = true;
    public bool ReturnsControlToUi { get; } = true;

    // wywoływane raz - wchodzę do stanu np. tura gracza się zaczęła
    public void Enter(CombatContext ctx)
    {
        Console.WriteLine("[?] PlayerStatusState");
    }

    // wywołuje się wiele razy - czy gracz JUŻ wykonał akcję?, co potem?
    public CombatStateTransition Update(CombatContext ctx)
    {
        return CombatStateTransition.Next(CombatStateType.EnemyTurn);
    }

    // wywoływane raz - sprzątanie po stanie - np. kończę turę
    public void Exit(CombatContext ctx) { }
}
