namespace Game.Core.Domain.Combat.States;

public partial class PlayerTurn : ICombatState
{
    public CombatStateType Type => CombatStateType.PlayerTurn;

    public void Enter(CombatContext ctx) { }

    public CombatStateTransition Update(CombatContext ctx)
    {
        return CombatStateTransition.Next(CombatStateType.PlayerStatus);
    }

    public void Exit(CombatContext ctx) { }
}
