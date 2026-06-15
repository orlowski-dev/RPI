namespace Game.Core.Application.Combat.Requests;

// bez statemachine bo nie potrzebuje juz ruszac tur
public record ClaimCombatRewardRequest(CombatSession Session, CombatReward Reward);
