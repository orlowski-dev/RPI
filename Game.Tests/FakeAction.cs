public class FakeAction : CombatAction
{
    public bool Executed { get; private set; }

    public override Result Execute(CombatSession session)
    {
        Executed = true;
        return Result.Success();
    }
}
