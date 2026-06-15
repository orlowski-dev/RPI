namespace Game.Core.Application.Combat.UseCases;

public class ClaimCombatRewardUseCase
    : IUseCase<ClaimCombatRewardRequest, ClaimCombatRewardResponse>
{
    public Result<ClaimCombatRewardResponse> Execute(ClaimCombatRewardRequest request)
    {
        request.StateMachine.Update(request.Session.Context);

        var reward = request.Reward;

        // dodać reward do gracza

        return Result<ClaimCombatRewardResponse>.Success(new());
    }
}
