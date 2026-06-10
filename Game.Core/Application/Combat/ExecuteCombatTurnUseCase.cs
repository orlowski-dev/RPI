namespace Game.Core.Application.Combat;

public class ExecuteCombatTurnUseCase : IUseCase<ExecuteCombatTurnRequest>
{
    public Result Execute(ExecuteCombatTurnRequest req)
    {
        req.Session.SelectAction(req.Action);
        req.Session.ExecuteSelectedAction();

        return Result.Success();
    }
}
