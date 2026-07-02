public record ArenaOnViewReadyVM(CombatSession CombatSession);

public record ArenaOnAttackVM(ResolveCombatResponse TurnResponse);

public record ArenaOnCombatFinishedVM(CombatReward Reward);
