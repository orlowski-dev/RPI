public class DungeonFactory
{
    private EncounterFactory _encounterF;

    public DungeonFactory()
    {
        _encounterF = new EncounterFactory();
    }

    public IReadOnlyList<Encounter> Create()
    {
        return _encounterF.CreateMany();
    }
}
