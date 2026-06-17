namespace Game.Core.Domain.Exploration;

public class Dungeon
{
    public readonly Guid Id = Guid.NewGuid();
    private IReadOnlyList<Encounter> _encounters;
    private DungeonFactory _factory;

    public IReadOnlyList<Encounter> Encounters => _encounters;

    public Dungeon()
    {
        _factory = new DungeonFactory();
        _encounters = _factory.Create();
    }

    public Dungeon(Guid id, IReadOnlyList<Encounter> encounters)
    {
        _factory = new DungeonFactory();

        Id = id;
        _encounters = encounters;
    }
}
