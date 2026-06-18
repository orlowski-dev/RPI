public class ClaimCombatRewardUseCase
    : IUseCase<ClaimCombatRewardRequest, ClaimCombatRewardResponse>
{
    public Result<ClaimCombatRewardResponse> Execute(ClaimCombatRewardRequest request)
    {
        if (request.Reward is null)
        {
            return Result<ClaimCombatRewardResponse>.Fail(
                new Error(
                    Code: "combat:claimReward",
                    Message: "request.Reward is null!",
                    Type: ErrorType.Validation
                )
            );
        }

        if (request.Session.RewardClaimed)
        {
            return Result<ClaimCombatRewardResponse>.Fail(
                new Error(
                    Code: "combat:claimReward",
                    Message: "Reward already claimed!",
                    Type: ErrorType.Validation
                )
            );
        }

        request.Session.ClaimReward(request.Reward);

        var reward = request.Reward;

        return Result<ClaimCombatRewardResponse>.Success(new());
    }
}
