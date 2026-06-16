namespace Game.Tests.Combat;

public class TestCombatParticipant
{
    public Player Player { get; } =
        new(id: "player", stats: new(30, 10, 5, 2, 3), type: PlayerType.Warrior);

    public Enemy Enemy1 { get; } =
        new(
            goldReward: 1,
            expReward: 1,
            id: "enemy_1",
            stats: new(20, 5, 3, 2, 3),
            rank: EnemyRank.Normal,
            type: EnemyType.Goblin
        );

    public Enemy Enemy2 { get; } =
        new(
            goldReward: 1,
            expReward: 1,
            id: "enemy_2",
            stats: new(10, 3, 1, 2, 3),
            rank: EnemyRank.Normal,
            type: EnemyType.Goblin
        );
}
