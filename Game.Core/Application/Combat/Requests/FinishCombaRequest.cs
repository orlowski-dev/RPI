namespace Game.Core.Application.Combat.Requests;

public record FinishCombatRequest(CombatSession Session, CombatStateMachine StateMachine);
