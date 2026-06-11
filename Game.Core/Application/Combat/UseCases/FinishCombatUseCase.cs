namespace Game.Core.Application.Combat;

public class FinishCombatUseCase : IUseCase<FinishCombatRequest>
{
    public Result Execute(FinishCombatRequest request)
    {
        // todo: potem przekazać reward w requescie
        request.Session.Finish(new CombatReward(10, 10, new[] { "sword" }));

        return Result.Success();
    }
}
