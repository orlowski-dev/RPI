namespace Game.Core.Domain.Combat.States;

public class PlayerTurnState : ICombatState
{
    public CombatStateType Type => CombatStateType.PlayerTurn;
    public bool IsAutomatic { get; } = false;

    public void Enter(CombatContext ctx)
    {
        var player = ctx.Session.Player;

        if (player is null)
        {
            throw new InvalidOperationException();
        }

        ctx.Session.SetActiveParticipant(player);
    }

    public CombatStateTransition Update(CombatContext ctx)
    {
        if (!ctx.Session.HasSelectedAction)
        {
            return CombatStateTransition.Stay();
        }

        ctx.Session.ExecuteSelectedAction();

        return CombatStateTransition.Next(CombatStateType.EnemyTurn);
    }

    public void Exit(CombatContext ctx)
    {
        ctx.Session.ClearTarget();
    }
}
