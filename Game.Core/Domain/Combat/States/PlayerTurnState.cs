namespace Game.Core.Domain.Combat.States;

public class PlayerTurnState : ICombatState
{
    public CombatStateType Type => CombatStateType.PlayerTurn;

    public void Enter(CombatContext ctx)
    {
        var player = ctx.Session.Player;

        if (player is null)
        {
            throw new InvalidOperationException();
        }

        ctx.Session.SetActiveParticipant(player);
        ctx.Session.SetTarget(null);
    }

    public CombatStateTransition Update(CombatContext ctx)
    {
        if (!ctx.Session.HasSelectedAction)
        {
            return CombatStateTransition.Stay();
        }

        ctx.Session.ExecuteSelectedAction();

        return CombatStateTransition.Next(CombatStateType.PlayerStatus);
    }

    public void Exit(CombatContext ctx) { }
}
