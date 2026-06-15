namespace Game.Core.Domain.Combat.States;

public class PlayerTurnState : ICombatState
{
    public CombatStateType Type => CombatStateType.PlayerTurn;
    public bool ReturnsControlToUi { get; } = false;

    public void Enter(CombatContext ctx)
    {
        Log.Write(this, "Entering..");

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
            Log.Write(this, "Has not selected action..");
            return CombatStateTransition.Stay();
        }

        ctx.Session.ExecuteSelectedAction();

        return CombatStateTransition.Next(CombatStateType.PlayerStatus);
    }

    public void Exit(CombatContext ctx) { }
}
