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
        ctx.Session.SetTarget(ctx.Session.Player);
    }

    public CombatStateTransition Update(CombatContext ctx)
    {
        //tymczaowe AI
        ctx.Session.SelectAction(new AttackAction());
        return CombatStateTransition.Next(CombatStateType.ResolveTurn);
    }

    public void Exit(CombatContext ctx) { }
}
