public class FinishCombatUseCase : IUseCase<FinishCombatRequest, FinishCombatResponse>
{
    private readonly IGameSessionProvider _gsProvider;
    private readonly CombatStateMachine _stateMachine;

    public FinishCombatUseCase(IGameSessionProvider gsProvider, CombatStateMachine stateMachine)
    {
        _gsProvider = gsProvider;
        _stateMachine = stateMachine;
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
            defeatedEnemies: _gsProvider.Current.CombatSession.AllEnemies
        );
        _gsProvider.Current.CombatSession.Finish(reward);
        var response = new FinishCombatResponse(Reward: reward);

        return Result<FinishCombatResponse>.Success(response);
    }
}
