namespace Game.Core.Application.Combat;

public record ExecuteCombatTurnRequest(CombatSession Session, CombatAction Action);
