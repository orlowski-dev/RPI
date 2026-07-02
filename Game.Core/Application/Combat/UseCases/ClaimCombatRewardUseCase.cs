public class ClaimCombatRewardUseCase
    : IUseCase<ClaimCombatRewardRequest, ClaimCombatRewardResponse>
{
    private readonly IGameSessionProvider _gs;
    private CombatSession? CombatSession => _gs.Current?.CombatSession;

    public ClaimCombatRewardUseCase(IGameSessionProvider gs)
    {
        _gs = gs;
    }

    public Result<ClaimCombatRewardResponse> Execute(ClaimCombatRewardRequest request)
    {
        if (_gs.Current is null || CombatSession is null)
        {
            DebugExtension.Fatal(this, "Game or Combat session is null!");
        }

        if (CombatSession.Reward is null)
        {
            return Result<ClaimCombatRewardResponse>.Fail(
                new Error(
                    Code: "combat:claimReward",
                    Message: "request.Reward is null!",
                    Type: ErrorType.Validation
                )
            );
        }

        if (CombatSession.RewardClaimed)
        {
            return Result<ClaimCombatRewardResponse>.Fail(
                new Error(
                    Code: "combat:claimReward",
                    Message: "Reward already claimed!",
                    Type: ErrorType.Validation
                )
            );
        }

        foreach (var item in CombatSession.Reward.Items)
        {
            new AddItemToInventoryUseCase().Execute(
                new AddItemToInventoryRequest(_gs.Current, item)
            );
        }
        CombatSession.ClaimReward(CombatSession.Reward);

        return Result<ClaimCombatRewardResponse>.Success(new());
    }
}
