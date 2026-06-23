public record ResolveCombatResponse(
    CombatStateType State,
    bool CombatFinished,
    string ActorId,
    string? NextActorId
);
