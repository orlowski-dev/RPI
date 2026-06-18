public class DungeonFactory
{
    private EncounterFactory _encounterF;

    public DungeonFactory()
    {
        _encounterF = new EncounterFactory();
    }

    public Dungeon Create()
    {
        return new(id: Guid.NewGuid(), encounters: _encounterF.CreateMany());
    }
}
