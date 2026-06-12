namespace Game.Core.Domain.Combat.States;

public class EnemyStatusState : ICombatState
{
    public CombatStateType Type => CombatStateType.EnemyStatus;

    // public bool IsAutomatic { get; } = true;
    public bool ReturnsControlToUi { get; } = true;

    // wywoływane raz - wchodzę do stanu np. tura gracza się zaczęła
    public void Enter(CombatContext ctx)
    {
        Console.WriteLine("[?] EnemyStatusState");
    }

    // wywołuje się wiele razy - czy gracz JUŻ wykonał akcję?, co potem?
    public CombatStateTransition Update(CombatContext ctx)
    {
        if (ctx.Session.NextParticipant.Type == CombatParticipantType.Enemy)
        {
            Console.WriteLine($"[? {this.GetType().Name}]: nextState: EnemyTurn");
            return CombatStateTransition.Next(CombatStateType.EnemyTurn);
        }

        Console.WriteLine($"[? {this.GetType().Name}]: nextState: PlayerTurn");
        return CombatStateTransition.Next(CombatStateType.PlayerTurn);
    }

    // wywoływane raz - sprzątanie po stanie - np. kończę turę
    public void Exit(CombatContext ctx) { }
}
