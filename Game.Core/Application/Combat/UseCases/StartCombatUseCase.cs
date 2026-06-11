using Game.Core.Domain.Combat.States;

namespace Game.Core.Application.Combat;

/// <summary>
/// Tworzy sesję walki.
/// </sumary>
public class StartCombatUseCase : IUseCase<StartCombatRequest, StartCombatResponse>
{
    public Result<StartCombatResponse> Execute(StartCombatRequest request)
    {
        var states = new List<ICombatState>() { new PlayerTurnState(), new EnemyTurnState() };
        var stateMachine = new CombatStateMachine(states);
        var session = new CombatSession([request.Player, .. request.Enemies]);
        stateMachine.Start(session.Context);

        return Result<StartCombatResponse>.Success(new(session, stateMachine));
    }
}
