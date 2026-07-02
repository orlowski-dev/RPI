/// <summary>
/// Tworzy sesję walki.
/// </sumary>
public class StartCombatUseCase : IUseCase<StartCombatRequest, StartCombatResponse>
{
    private IGameSessionProvider _gsProvider;
    private readonly CombatStateMachine _stateMachine;

    public StartCombatUseCase(
        IGameSessionProvider gsProvider,
        CombatStateMachine combatStateMachine
    )
    {
        _gsProvider = gsProvider;
        _stateMachine = combatStateMachine;
    }

    public Result<StartCombatResponse> Execute(StartCombatRequest request)
    {
        if (request.Enemies.Count() == 0)
        {
            return Result<StartCombatResponse>.Fail(
                new("List of enemies is empty!", ErrorType.Validation)
            );
        }

        var session = new CombatSession([request.Player, .. request.Enemies]);
        _stateMachine.Start(session.Context);

        if (_gsProvider.Current is null)
        {
            return Result<StartCombatResponse>.Fail(
                new("Game session provider has no session!", ErrorType.InvalidState)
            );
        }

        _gsProvider.Current.SetCombatSession(session);

        return Result<StartCombatResponse>.Success(new(session, _stateMachine));
    }
}
