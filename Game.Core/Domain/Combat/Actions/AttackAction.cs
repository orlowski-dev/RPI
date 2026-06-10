namespace Game.Core.Domain.Combat.Actions;

public class AttackAction : CombatAction
{
    public override Result Execute(CombatSession session)
    {
        if (session.Target is null)
        {
            return Result.Fail(
                new("combat_action.attact.execute", "Target is not set!", ErrorType.Validation)
            );
        }

        session.Target.ReceiveDamage(session.ActiveParticipant.Stats.Attack);

        return Result.Success();
    }
}
