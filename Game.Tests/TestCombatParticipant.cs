public class TestCombatParticipant
{
    public Player Player =>
        new(name: "Player", stats: new(30, 10, 5, 2, 3), type: PlayerType.Warrior);

    public Player GetPlayer(Inventory? inventory = null) =>
        new(
            name: "Player",
            stats: new(30, 10, 5, 2, 3),
            type: PlayerType.Warrior,
            inventory: inventory
        );

    public Enemy Enemy1 { get; } =
        new(
            name: "Goblin",
            goldReward: 1,
            expReward: 1,
            stats: new(20, 5, 3, 2, 3),
            rank: EnemyRank.Normal,
            type: EnemyType.Jolleen,
            subType: EnemySubType.Jolleen
        );

    public Enemy Enemy2 { get; } =
        new(
            name: "Ork",
            goldReward: 1,
            expReward: 1,
            stats: new(10, 3, 1, 2, 3),
            rank: EnemyRank.Normal,
            type: EnemyType.Maw,
            subType: EnemySubType.Maw
        );
}
