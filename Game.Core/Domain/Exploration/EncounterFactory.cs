public class EncounterFactory
{
    private static readonly Random _random = Random.Shared;
    private int _minEncounters = 1;
    private int _maxEncounter = 5;
    private int _encountersToGenerate;
    private EnemyFactory _enemyFactory = new();
    private bool _hasBoss = false;

    public IReadOnlyList<Encounter> CreateMany()
    {
        _encountersToGenerate = _random.Next(_minEncounters, _maxEncounter + 1);
        return GenerateEncounters();
    }

    private IReadOnlyList<Encounter> GenerateEncounters()
    {
        List<Encounter> temp = new();
        for (var i = 0; i < _encountersToGenerate; i++)
        {
            var enemies = GetEnemies();
            var hasBoss = enemies.Any((enemy) => enemy.Rank == EnemyRank.Boss);
            temp.Add(
                new Encounter(
                    enemies: enemies,
                    state: hasBoss ? EncounterState.Locked : EncounterState.Available
                )
            );
        }

        return temp;
    }

    private IReadOnlyList<Enemy> GetEnemies()
    {
        var count = Random.Shared.Next(1, 4);
        var enemies = new List<Enemy>();

        for (var i = 0; i < count; i++)
        {
            if (!_hasBoss)
            {
                enemies.Add(_enemyFactory.CreateBossEnemy());
                _hasBoss = true;
                continue;
            }
            enemies.Add(_enemyFactory.CreateNonBossEnemy());
        }

        return enemies;
    }
}
