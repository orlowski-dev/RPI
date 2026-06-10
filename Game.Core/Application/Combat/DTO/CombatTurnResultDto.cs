namespace Game.Core.Application.Combat;

public class CombatTurnResultDto
{
    public CombatStateType State { get; init; }
    public bool CombatFinished { get; init; }
    public string CurrentActorId { get; init; }

    public CombatTurnResultDto(CombatStateType state, bool combatFinished, string currentActorId)
    {
        State = state;
        CombatFinished = combatFinished;
        CurrentActorId = currentActorId;
    }
}
