namespace Game.Core.Application.Combat;

public record StartCombatResponse(CombatSession CombatSession, CombatStateMachine StateMachine);
