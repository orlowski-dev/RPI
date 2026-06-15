using Game.Core.Domain.Combat.Actions;

namespace Game.Core.Domain.Combat.States;

public class EnemyTurnState : ICombatState
{
    public CombatStateType Type => CombatStateType.EnemyTurn;

    // public bool IsAutomatic { get; } = true;
    public bool ReturnsControlToUi { get; } = false;

    public void Enter(CombatContext ctx)
    {
        Log.Write(this, "Entering..");

        ctx.Session.MoveNextParticipant();

        var player = ctx.Session.Player;

        ctx.Session.SetTarget(player);
    }

    public CombatStateTransition Update(CombatContext ctx)
    {
        //tymczaowe AI
        ctx.Session.SelectAction(new AttackAction());
        ctx.Session.ExecuteSelectedAction();

        return CombatStateTransition.Next(CombatStateType.EnemyStatus);
    }

    public void Exit(CombatContext ctx) { }
}
