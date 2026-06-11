namespace Game.Core.Application.Combat;

public class ExecuteCombatTurnUseCase : IUseCase<ExecuteCombatTurnRequest>
{
    public Result Execute(ExecuteCombatTurnRequest req)
    {
        req.Session.SelectAction(req.Action);
        req.StateMachine.Update(req.Session.Context);

        return Result.Success();
    }
}
