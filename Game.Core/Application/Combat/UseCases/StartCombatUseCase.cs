namespace Game.Core.Application.Combat.UseCases;

/// <summary>
/// Tworzy sesję walki.
/// </sumary>
public class StartCombatUseCase : IUseCase<StartCombatRequest, StartCombatResponse>
{
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

        return Result<StartCombatResponse>.Success(new(session, stateMachine));
    }
}
