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

        var target = session.Target;
        var attacker = session.ActiveParticipant;
        var crit = attacker.RollCrit();
        var damage = session.Target.ReceiveDamage(attacker.Stats.Attack, crit);

        var msg = $"{attacker.Name} atakuje {target.Name} i zadaje {damage} obrażeń.";

        if (crit)
        {
            msg += " (Crit)";
        }

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
