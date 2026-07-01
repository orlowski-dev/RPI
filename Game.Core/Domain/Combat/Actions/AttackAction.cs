public class AttackAction : CombatAction
{
    public override Result<CombatActionResult> Execute(CombatSession session)
    {
        Console.WriteLine(
            $"[AttackAction] Active={session.ActiveParticipant.Name}, Target={session.Target?.Name ?? "NULL"}"
        );

        if (session.Target is null)
        {
            return Result<CombatActionResult>.Fail(new("Target is not set!", ErrorType.Validation));
        }

        var damage = session.ActiveParticipant.Stats.Attack;
        session.Target.ReceiveDamage(damage);
        var target = session.Target;
        var attacker = session.ActiveParticipant;

        var msg =
            $"{attacker.Name} zaatakował {target.Name} i zadał {damage} damage ({target.Stats.CurrentHp}/{target.Stats.MaxHp}hp).";

        var result = new CombatActionResult(
            Attacker: attacker,
            Target: target,
            Value: damage,
            Type: ActionType.Attack,
            Message: msg
        );
        return Result<CombatActionResult>.Success(result);
    }
}
