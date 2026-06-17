using Game.Core.Domain.Exploration;

namespace Game.Core.Domain.Session;

public class GameSession
{
    public Player Player { get; }
    public GameSessionState State { get; private set; }
    public CombatSession? CombatSession { get; private set; }
    public Dungeon? Dungeon { get; private set; }

    // inventory
    // slot
    // dung
    // combat
    public GameSession(Player player)
    {
        State = GameSessionState.MainMenu;
        Player = player;
    }
}
