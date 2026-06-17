namespace Game.Core.Domain.Actors;

public class PlayerFactory
{
    public Player Create(PlayerFactoryRequests.Create req)
    {
        return new Player(stats: req.Stats, type: req.Type);
    }
}
