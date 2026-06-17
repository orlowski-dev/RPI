using Game.Core.Domain.Actors.Requests;
using Game.Core.Domain.Session.Requests;

namespace Game.Core.Domain.Session;

public class GameSessionFactory
{
    private PlayerFactory _playerFactory;

    public GameSessionFactory()
    {
        _playerFactory = new PlayerFactory();
    }

    public GameSession Create(CreateGameSessionRequest req)
    {
        var player = _playerFactory.Create(req.Player);
        return new GameSession(player);
    }
}
