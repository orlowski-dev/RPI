using Game.Core.Domain.Exploration;

namespace Game.Core.Domain.Session;

public class GameSession
{
    public readonly Guid Id;
    public Player Player { get; }
    public GameSessionState State { get; private set; }
    public CombatSession? CombatSession { get; private set; }
    public Dungeon? Dungeon { get; private set; }
    public List<string>? Inventory { get; private set; }

    public GameSession(Player player)
    {
        Id = Guid.NewGuid();
        State = GameSessionState.MainMenu;
        Player = player;
    }
}
