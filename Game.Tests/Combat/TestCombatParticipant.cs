public class TestCombatParticipant
{
    public Player Player { get; } =
        new(
            id: "player",
            baseStats: new(MaxHp: 30, Attack: 10, Defense: 5, CriticalChance: 2, Luck: 3)
        );

    public Enemy Enemy1 { get; } =
        new(
            id: "enemy_1",
            baseStats: new(MaxHp: 20, Attack: 5, Defense: 3, CriticalChance: 2, Luck: 3)
        );

    public Enemy Enemy2 { get; } =
        new(
            id: "enemy_2",
            baseStats: new(MaxHp: 10, Attack: 3, Defense: 1, CriticalChance: 2, Luck: 3)
        );
}
