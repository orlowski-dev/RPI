namespace Game.Core.Domain.Combat.States;

public enum CombatStateType
{
    Start,
    PlayerTurn,
    EnemyTurn,
    ResolveTurn,
    Reward,
    PlayerDeath,
    End,
}
