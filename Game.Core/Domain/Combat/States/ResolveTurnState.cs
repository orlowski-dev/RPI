public class ResolveTurnState : ICombatState
{
    public CombatStateType Type => CombatStateType.ResolveTurn;

    public bool ReturnsControlToUi { get; } = false;

    // wywoływane raz - wchodzę do stanu np. tura gracza się zaczęła
    public void Enter(CombatContext ctx)
    {
        // wykonanie ruchu
        if (ctx.Session.HasSelectedAction)
        {
            ctx.Session.ExecuteSelectedAction();
        }

        ctx.Session.UpdateStatus();
    }

    // wywołuje się wiele razy - czy gracz JUŻ wykonał akcję? co potem?
    public CombatStateTransition Update(CombatContext ctx)
    {
        if (ctx.Session.ActiveParticipant == null)
        {
            throw new InvalidOperationException("No active participant.");
        }

        if (ctx.Session.IsFinished)
        {
            if (ctx.Session.PlayerWon)
            {
                return CombatStateTransition.Next(CombatStateType.Reward);
            }

            return CombatStateTransition.Next(CombatStateType.PlayerDeath);
        }

        var next = ctx.Session.NextParticipant;

        if (next is null)
        {
            throw new InvalidOperationException();
        }

        if (next is Enemy)
        {
            return CombatStateTransition.Next(CombatStateType.EnemyTurn);
        }

        return CombatStateTransition.Next(CombatStateType.PlayerTurn);
    }

    // wywoływane raz - sprzątanie po stanie - np. kończę turę
    public void Exit(CombatContext ctx)
    {
        ctx.Session.MoveToNextParticipant();
    }
}
