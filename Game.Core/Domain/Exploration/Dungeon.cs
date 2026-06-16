namespace Game.Core.Domain.Exploration;

public class Dungeon
{
    public readonly Guid Id = Guid.NewGuid();
    private IReadOnlyList<Encounter> _encounters;

    public Dungeon()
    {
        _encounters = [new Encounter(GenerateEnemies())];
    }

    private IReadOnlyList<Enemy> GenerateEnemies()
    {
        return
        [
            new(
                goldReward: 1,
                expReward: 1,
                id: "enemy_1",
                stats: new(20, 5, 3, 2, 3),
                rank: EnemyRank.Normal,
                type: EnemyType.Goblin
            ),
            new(
                goldReward: 1,
                expReward: 1,
                id: "enemy_2",
                stats: new(10, 3, 1, 2, 3),
                rank: EnemyRank.Normal,
                type: EnemyType.Ork
            ),
        ];
    }
}
