namespace Game.Core.Domain.Actors.Requests;

public class PlayerFactory
{
    public Player Create(CreatePlayerReques req)
    {
        return new Player(stats: req.Stats, type: req.Type);
    }
}
