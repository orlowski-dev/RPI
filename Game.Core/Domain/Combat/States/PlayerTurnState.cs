namespace Game.Core.Domain.Combat.States;

public class PlayerTurnState : ICombatState
{
    public CombatStateType Type => CombatStateType.PlayerTurn;
    public bool ReturnsControlToUi { get; } = false;

    public void Enter(CombatContext ctx)
    {
        Log.Write(this, "Entering..");

        ctx.Session.ClearTarget();
        ctx.Session.ClearSelectedAction();
    }

    public CombatStateTransition Update(CombatContext ctx)
    {
        if (!ctx.Session.HasSelectedAction)
        {
            Log.Write(this, "Has not selected action..");
            return CombatStateTransition.Stay();
        }

        return CombatStateTransition.Next(CombatStateType.ResolveTurn);
    }

    public void Exit(CombatContext ctx) { }
}
