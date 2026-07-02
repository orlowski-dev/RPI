public class FakeAction : CombatAction
{
    public bool Executed { get; private set; }

    public override Result<bool> Execute(CombatSession session)
    {
        Executed = true;
        return Result<bool>.Success(true);
    }
}
