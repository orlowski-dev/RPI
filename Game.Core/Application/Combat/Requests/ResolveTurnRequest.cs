namespace Game.Core.Application.Combat;

public record ResolveTurnRequest(
    CombatSession Session,
    CombatStateMachine StateMachine,
    CombatAction? Action = null
);
