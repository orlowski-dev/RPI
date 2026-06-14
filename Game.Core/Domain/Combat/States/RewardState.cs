namespace Game.Core.Domain.Combat.States;

public class RewardState : ICombatState
{
    public CombatStateType Type { get; } = CombatStateType.Reward;

    // public bool IsAutomatic { get; }
    public bool ReturnsControlToUi { get; } = true;

    // wywoływane raz - wchodzę do stanu np. tura gracza się zaczęła
    public void Enter(CombatContext ctx)
    {
        Console.WriteLine("[?] RewardState");
    }

    // wywołuje się wiele razy - czy gracz JUŻ wykonał akcję? co potem?
    public CombatStateTransition Update(CombatContext ctx)
    {
        return CombatStateTransition.Stay();
    }

    // wywoływane raz - sprzątanie po stanie - np. kończę turę
    public void Exit(CombatContext ctx) { }
}
