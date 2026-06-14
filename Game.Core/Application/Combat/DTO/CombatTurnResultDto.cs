namespace Game.Core.Application.Combat.DTO;

public class CombatTurnResultDto
{
    public CombatStateType State { get; init; }
    public bool CombatFinished { get; init; }
    public string ActorId { get; init; }
    public string? NextActorId { get; init; }

    public CombatTurnResultDto(
        CombatStateType state,
        bool combatFinished,
        string actorId,
        string? nextActorId = null
    )
    {
        State = state;
        CombatFinished = combatFinished;
        ActorId = actorId;
        NextActorId = nextActorId;
    }
}
