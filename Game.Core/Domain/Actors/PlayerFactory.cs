using Game.Core.Domain.Actors.Definitions;

namespace Game.Core.Domain.Actors.Requests;

public class PlayerFactory
{
    public Player Create(CreatePlayerRequest req)
    {
        var def = PlayerDefinitions.Values[req.Type];
        return new Player(name: req.Name, stats: def.BaseStats, type: req.Type);
    }
}
