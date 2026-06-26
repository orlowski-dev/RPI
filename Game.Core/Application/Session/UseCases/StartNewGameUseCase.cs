public class StartNewGameUseCase : IUseCase<StartNewGameRequest, StartNewGameResponse>
{
    private readonly IGameSessionProvider _session;
    private readonly GameSessionFactory _factory;

    public StartNewGameUseCase(IGameSessionProvider session, GameSessionFactory factory)
    {
        _session = session;
        _factory = factory;
    }

    public Result<StartNewGameResponse> Execute(StartNewGameRequest req)
    {
        var gSession = _factory.Create(playerName: req.PlayerName, playerType: req.PlayerType);
        _session.Set(gSession);

        return Result<StartNewGameResponse>.Success(
            new StartNewGameResponse(GameSession: gSession)
        );
    }
}
