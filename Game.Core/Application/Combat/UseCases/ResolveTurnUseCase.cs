namespace Game.Core.Application.Combat;

public class ResolveTurnUseCase : IUseCase<ResolveTurnRequest, CombatTurnResultDto>
{
    public Result<CombatTurnResultDto> Execute(ResolveTurnRequest req)
    {
        if (req.Action is not null)
        {
            req.Session.SelectAction(req.Action);
        }

        req.StateMachine.Update(req.Session.Context);
        req.Session.UpdateStatus();

        var currentActorId = req.Session.ActiveParticipant.Id;

        // dto opoisuje wynik sesji, a nie wynik contextu
        // nie używaj Context.Session poza StateMachine/State!
        var dto = new CombatTurnResultDto(
            req.Session.State,
            req.Session.IsFinished,
            currentActorId
        );

        // todo: na razie zrwadane dto po wszystkich zakończonych podturach - zmienić filozofię..

        return Result<CombatTurnResultDto>.Success(dto);
    }
}
