public class FinishCombatUseCase : IUseCase<FinishCombatRequest, FinishCombatResponse>
{
    private readonly IGameSessionProvider _gsProvider;
    private readonly CombatStateMachine _stateMachine;
    private readonly ClaimCombatRewardUseCase _crUseCase;

    public FinishCombatUseCase(
        IGameSessionProvider gsProvider,
        CombatStateMachine stateMachine,
        ClaimCombatRewardUseCase claimCombatRewardUseCase
    )
    {
        _gsProvider = gsProvider;
        _stateMachine = stateMachine;
        _crUseCase = claimCombatRewardUseCase;
    }

    public Result<FinishCombatResponse> Execute(FinishCombatRequest request)
    {
        if (_gsProvider.Current is null || _gsProvider.Current.CombatSession is null)
        {
            DebugExtension.Fatal(this, "Game session or/and combat session are null!");
        }

        _stateMachine.Update(_gsProvider.Current.CombatSession.Context);

        if (!_gsProvider.Current.CombatSession.IsFinished)
        {
            return Result<FinishCombatResponse>.Fail(
                new("Combat is not finished!", ErrorType.InvalidState)
            );
            ;
        }

        var reward = new RewardCalculator().Calculate(
            defeatedEnemies: _gsProvider.Current.CombatSession.AllEnemies,
            playerLevel: _gsProvider.Current.Player.Level
        );
        _gsProvider.Current.CombatSession.Finish(reward);
        var response = new FinishCombatResponse(Reward: reward);
        _crUseCase.Execute(new());

        return Result<FinishCombatResponse>.Success(response);
    }
}
