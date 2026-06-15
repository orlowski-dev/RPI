namespace Game.Core.Application.Combat.Requests;

public record ClaimCombatRewardRequest(
    CombatSession Session,
    CombatStateMachine StateMachine,
    CombatReward Reward
);
