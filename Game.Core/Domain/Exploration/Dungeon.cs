public class Dungeon
{
    public readonly Guid Id = Guid.NewGuid();
    private IReadOnlyList<Encounter> _encounters;

    public IReadOnlyList<Encounter> Encounters => _encounters;

    public Dungeon(Guid id, IReadOnlyList<Encounter> encounters)
    {
        Id = id;
        _encounters = encounters;
    }
}
