/// <summary>
/// Tworzy sesję walki.
/// </sumary>
public class StartCombatUseCase : IUseCase<StartCombatRequest, StartCombatResponse>
{
    private IGameSessionProvider _gsProvider = null!;

    public StartCombatUseCase(IGameSessionProvider gsProvider)
    {
        _gsProvider = gsProvider;
    }

    public Result<StartCombatResponse> Execute(StartCombatRequest request)
    {
        if (request.Enemies.Count() == 0)
        {
            return Result<StartCombatResponse>.Fail(
                new("List of enemies is empty!", ErrorType.Validation)
            );
        }

        var states = new List<ICombatState>()
        {
            new PlayerTurnState(),
            new EnemyTurnState(),
            new ResolveTurnState(),
            new RewardState(),
            new PlayerDeathState(),
        };
        var stateMachine = new CombatStateMachine(states);
        var session = new CombatSession([request.Player, .. request.Enemies]);
        stateMachine.Start(session.Context);

        if (_gsProvider.Current is null)
        {
            return Result<StartCombatResponse>.Fail(
                new("Game session provider has no session!", ErrorType.InvalidState)
            );
        }

        _gsProvider.Current.SetCombatSession(session);

        return Result<StartCombatResponse>.Success(new(session, stateMachine));
    }
}
