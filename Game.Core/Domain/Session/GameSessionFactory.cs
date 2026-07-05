public class GameSessionFactory
{
    private PlayerFactory _playerFactory;

    public GameSessionFactory()
    {
        _playerFactory = new PlayerFactory();
    }

    public GameSession Create(string playerName, PlayerType playerType)
    {
        var player = _playerFactory.Create(name: playerName, playerType: playerType);
        return new GameSession(player);
    }
}
