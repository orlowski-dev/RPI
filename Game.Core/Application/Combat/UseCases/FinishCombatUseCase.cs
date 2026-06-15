using Game.Core.Application.Combat.Requests;

namespace Game.Core.Application.Combat.UseCases;

public class FinishCombatUseCase : IUseCase<FinishCombatRequest>
{
    public Result Execute(FinishCombatRequest request)
    {
        request.StateMachine.Update(request.Session.Context);

        if (!request.Session.IsFinished)
        {
            return Result.Fail(
                new(
                    "combat:finishUseCase:Execute",
                    "Combat is not finished!",
                    ErrorType.InvalidState
                )
            );
            ;
        }

        // todo: potem przekazać reward w requescie
        // request.Session.Finish(new CombatReward(10, 10, new[] { "sword" }));

        return Result.Success();
    }
}
