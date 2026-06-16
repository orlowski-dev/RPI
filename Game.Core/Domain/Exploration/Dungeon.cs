namespace Game.Core.Domain.Exploration;

public class Dungeon
{
    public readonly Guid Id = Guid.NewGuid();

    private static Random _random = new Random();
    private int _minEncounters = 1;
    private int _maxEncounter = 5;
    private int _encountersToGenerate;
    private bool _hasBossEncounter = false;
    private IReadOnlyList<Encounter> _encounters;

    public Dungeon()
    {
        _encountersToGenerate = _random.Next(_minEncounters, _maxEncounter + 1);
        _encounters = GenerateEncounters();

        DebugExtension.Log(this, $"Dugneon {Id} created. Encounters count: {_encounters.Count()}.");
        // DebugExtension.Log(this, DebugExtension.Dump(_encounters));
    }

    private IReadOnlyList<Encounter> GenerateEncounters()
    {
        List<Encounter> temp = new();
        for (var i = 0; i < _encountersToGenerate; i++)
        {
            if (!_hasBossEncounter)
            {
                temp.Add(
                    new([.. GenerateEnemies(), GenerateBossEnemy()], state: EncounterState.Locked)
                );
                _hasBossEncounter = true;
                continue;
            }

            temp.Add(new(GenerateEnemies()));
        }

        return temp;
    }

    private Enemy GenerateBossEnemy()
    {
        return new(
            goldReward: 1,
            expReward: 1,
            stats: new(20, 5, 3, 2, 3),
            rank: EnemyRank.Boss,
            type: EnemyType.Goblin
        );
    }

    private IReadOnlyList<Enemy> GenerateEnemies()
    {
        return
        [
            new(
                goldReward: 1,
                expReward: 1,
                stats: new(20, 5, 3, 2, 3),
                rank: EnemyRank.Normal,
                type: EnemyType.Goblin
            ),
            new(
                goldReward: 1,
                expReward: 1,
                stats: new(10, 3, 1, 2, 3),
                rank: EnemyRank.Normal,
                type: EnemyType.Ork
            ),
        ];
    }
}
