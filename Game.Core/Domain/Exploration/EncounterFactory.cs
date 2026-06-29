public class EncounterFactory
{
    private static Random _random = new Random();
    private int _minEncounters = 1;
    private int _maxEncounter = 5;
    private int _encountersToGenerate;
    private EnemyFactory _enemyFactory = new();

    public IReadOnlyList<Encounter> CreateMany()
    {
        _encountersToGenerate = _random.Next(_minEncounters, _maxEncounter + 1);
        return GenerateEncounters();
    }

    private IReadOnlyList<Encounter> GenerateEncounters()
    {
        List<Encounter> temp = new();
        for (var i = 0; i < _encountersToGenerate; i++) { }

        return temp;
    }
}
