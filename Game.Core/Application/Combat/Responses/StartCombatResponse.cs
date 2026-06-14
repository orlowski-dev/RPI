namespace Game.Core.Application.Combat.Responses;

public record StartCombatResponse(CombatSession CombatSession, CombatStateMachine StateMachine);
