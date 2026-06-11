namespace Game.Core.Application.Combat;

public class ResolveTurnUseCase : IUseCase<ResolveTurnRequest, CombatTurnResultDto>
{
    public Result<CombatTurnResultDto> Execute(ResolveTurnRequest req)
    {
        var currentActorId = "todo"; // np czyja tura bo do UI nie zwracam całej sesji P1 (Player1), E1, E2..
        req.Session.SelectAction(req.Action);
        req.StateMachine.Update(req.Session.Context);

        // dto opoisuje wynik sesji, a nie wynik contextu
        // nie używaj Context.Session poza StateMachine/State!
        var dto = new CombatTurnResultDto(
            req.Session.State,
            req.Session.IsFinished,
            currentActorId
        );

        return Result<CombatTurnResultDto>.Success(dto);
    }
}
