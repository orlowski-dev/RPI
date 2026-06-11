namespace Game.Core.Application.Combat;

public record ResolveTurnRequest(
    CombatSession Session,
    CombatAction Action,
    CombatStateMachine StateMachine
);
