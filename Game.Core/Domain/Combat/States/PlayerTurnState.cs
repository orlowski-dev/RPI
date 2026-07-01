public class PlayerTurnState : ICombatState
{
    public CombatStateType Type => CombatStateType.PlayerTurn;
    public bool ReturnsControlToUi { get; } = true;

    public void Enter(CombatContext ctx)
    {
        ctx.Session.ClearTarget();
        ctx.Session.ClearSelectedAction();
        ctx.Session.ClearLastAttack();
    }

    public CombatStateTransition Update(CombatContext ctx)
    {
        if (!ctx.Session.HasSelectedAction)
        {
            return CombatStateTransition.Stay();
        }

        return CombatStateTransition.Next(CombatStateType.ResolveTurn);
    }

    public void Exit(CombatContext ctx) { }
}
