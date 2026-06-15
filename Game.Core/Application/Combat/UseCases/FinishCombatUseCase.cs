using Game.Core.Application.Combat.DTO;
using Game.Core.Application.Combat.Requests;
using Game.Core.Domain.Combat.Reward;

namespace Game.Core.Application.Combat.UseCases;

public class FinishCombatUseCase : IUseCase<FinishCombatRequest, FinishCombatResponse>
{
    public Result<FinishCombatResponse> Execute(FinishCombatRequest request)
    {
        request.StateMachine.Update(request.Session.Context);

        if (!request.Session.IsFinished)
        {
            return Result<FinishCombatResponse>.Fail(
                new(
                    "combat:finishUseCase:Execute",
                    "Combat is not finished!",
                    ErrorType.InvalidState
                )
            );
            ;
        }

        var reward = new RewardCalculator().Calculate(defeatedEnemies: request.Session.AllEnemies);
        var dto = new CombatResult(Reward: reward);

        return Result<FinishCombatResponse>.Success(new(dto));
    }
}
