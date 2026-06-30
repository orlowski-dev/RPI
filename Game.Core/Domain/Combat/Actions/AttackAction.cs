public class AttackAction : CombatAction
{
    public override Result<int> Execute(CombatSession session)
    {
        Console.WriteLine(
            $"[AttackAction] Active={session.ActiveParticipant.Name}, Target={session.Target?.Name ?? "NULL"}"
        );

        if (session.Target is null)
        {
            Console.WriteLine("[AttackAction] FAILED - target is null");
            return Result<int>.Fail(new("Target is not set!", ErrorType.Validation));
        }

        var damage = session.ActiveParticipant.Stats.Attack;
        session.Target.ReceiveDamage(damage);
        var target = session.Target;
        DebugExtension.Log(
            this,
            $"{session.ActiveParticipant.Name} zaatakował {target.Name} i zadał {damage} damage ({target.Stats.CurrentHp}/{target.Stats.MaxHp}hp)."
        );

        return Result<int>.Success(damage);
    }
}
