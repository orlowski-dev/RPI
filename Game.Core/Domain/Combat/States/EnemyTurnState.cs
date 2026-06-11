using Game.Core.Domain.Combat.Actions;

namespace Game.Core.Domain.Combat.States;

public class EnemyTurnState : ICombatState
{
    public CombatStateType Type => CombatStateType.EnemyTurn;
    public bool IsAutomatic { get; } = true;

    public void Enter(CombatContext ctx)
    {
        Console.WriteLine("[?] EnemyTurnState");

        var player = ctx.Session.Player;
        var playerEnemies = ctx.Session.AliveEnemies;

        ctx.Session.SetTarget(player);
        ctx.Session.SetActiveParticipant(playerEnemies.First());
    }

    public CombatStateTransition Update(CombatContext ctx)
    {
        //tymczaowe AI
        ctx.Session.SelectAction(new AttackAction());
        ctx.Session.ExecuteSelectedAction();

        return CombatStateTransition.Next(CombatStateType.EnemyStatus);
    }

    public void Exit(CombatContext ctx)
    {
        ctx.Session.ClearTarget();
    }
}
