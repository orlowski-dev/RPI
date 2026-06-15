namespace Game.Core.Application.Combat.Requests;

public record ResolveTurnRequest(
    CombatSession Session,
    CombatStateMachine StateMachine,
    CombatAction? Action = null
);
