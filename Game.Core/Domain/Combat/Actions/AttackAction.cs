using Game.Core.Application.Results;

namespace Game.Core.Domain.Combat.Actions;

public partial class AttackAction : CombatAction
{
    public override Result Execute(CombatSession session)
    {
        return Result.Success();
    }
}
