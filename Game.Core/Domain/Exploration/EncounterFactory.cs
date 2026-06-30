public class EncounterFactory
{
    private static readonly Random _random = new Random();
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
            if (!_hasBoss)
            {
                temp.Add(new([_enemyFactory.CreateBossEnemy()], state: EncounterState.Locked));
                _hasBoss = true;
            }

            temp.Add(new([_enemyFactory.CreateNonBossEnemy()]));
        }

        return temp;
    }
}
