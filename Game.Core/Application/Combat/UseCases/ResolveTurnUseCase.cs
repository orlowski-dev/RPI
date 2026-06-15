namespace Game.Core.Application.Combat.UseCases;

public class ResolveTurnUseCase : IUseCase<ResolveTurnRequest, ResolveCombatResponse>
{
    public Result<ResolveCombatResponse> Execute(ResolveTurnRequest req)
    {
        if (req.Action is not null)
        {
            req.Session.SelectAction(req.Action);
        }

        req.StateMachine.Update(req.Session.Context);
        req.Session.UpdateStatus();

        var actorId = req.Session.ActiveParticipant.Id;
        var nextActorId = req.Session.NextParticipant?.Id ?? null;

        // dto opoisuje wynik sesji, a nie wynik contextu
        // nie używaj Context.Session poza StateMachine/State!
        var dto = new CombatTurnResultDto(
            state: req.Session.State,
            combatFinished: req.Session.IsFinished,
            actorId: actorId,
            nextActorId: nextActorId
        );

        var response = new ResolveCombatResponse(dto);

        return Result<ResolveCombatResponse>.Success(response);
    }
}
