namespace Game.Core.Domain.Session;

public class GameSession
{
    public Player Player { get; }

    // inventory
    // slot
    // dung
    // combat
    public GameSession(Player player)
    {
        Player = player;
    }
}
