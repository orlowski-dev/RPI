namespace Game.Core.Application.Combat;

/// <summary>
/// Tworzy sesję walki.
/// </sumary>
public class StartCombatUseCase : IUseCase<StartCombatRequest, CombatSession>
{
    public Result<CombatSession> Execute(StartCombatRequest request)
    {
        var session = new CombatSession([request.Player, .. request.Enemies]);

        return Result<CombatSession>.Success(session);
    }
}
