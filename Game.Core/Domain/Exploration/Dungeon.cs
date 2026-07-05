public class Dungeon
{
    public readonly Guid Id = Guid.NewGuid();
    private IReadOnlyList<Encounter> _encounters;

    public int FinishedEncounters => _encounters.Count((en) => en.IsFinished);
    public int EncountersLeft => _encounters.Count() - FinishedEncounters;

    public IReadOnlyList<Encounter> Encounters => _encounters;

    public Dungeon(Guid id, IReadOnlyList<Encounter> encounters)
    {
        Id = id;
        _encounters = encounters;
    }
}
