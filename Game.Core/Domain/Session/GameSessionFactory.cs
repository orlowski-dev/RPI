namespace Game.Core.Domain.Session;

public class GameSessionFactory
{
    private PlayerFactory _playerFactory;

    public GameSessionFactory()
    {
        _playerFactory = new PlayerFactory();
    }

    public GameSession CreateNew(GameSessionFactoryRequests.Create req)
    {
        var player = _playerFactory.Create(req.Player);
        return new GameSession(player);
    }
}
