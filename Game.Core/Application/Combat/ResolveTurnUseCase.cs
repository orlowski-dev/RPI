namespace Game.Core.Application.Combat;

public class ResolveTurnUseCase : IUseCase<ResolveTurnRequest, CombatTurnResultDto>
{
    public Result<CombatTurnResultDto> Execute(ResolveTurnRequest req)
    {
        var currentActorId = ""; // np czyja tura bo do UI nie zwracam całej sesji P1 (Player1), E1, E2..
        req.Session.SelectAction(req.Action);
        var context = new CombatContext(req.Session);
        req.StateMachine.Update(context);

        var dto = new CombatTurnResultDto(
            context.Session.State,
            context.Session.IsFinished,
            currentActorId
        );

        return Result<CombatTurnResultDto>.Success(dto);
    }
}
