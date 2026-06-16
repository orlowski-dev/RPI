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

        var damage = session.ActiveParticipant.Stats.Attack;
        session.Target.ReceiveDamage(damage);
        var target = session.Target;
        DebugExtension.Log(
            this,
            $"{session.ActiveParticipant.Id} zaatakował {target.Id} i zadał {damage} damage ({target.Stats.CurrentHp}/{target.Stats.MaxHp}hp)."
        );

        return Result.Success();
    }
}
