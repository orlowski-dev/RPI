public class StartNewGameUseCase : IUseCase<StartNewGameRequest, StartNewGameResponse>
{
    public Result<StartNewGameResponse> Execute(StartNewGameRequest req)
    {
        var gSessionF = new GameSessionFactory();
        var gSession = gSessionF.Create(playerName: req.PlayerName, playerType: req.PlayerType);
        var response = new StartNewGameResponse(GameSession: gSession);
        return Result<StartNewGameResponse>.Success(response);
    }
}
