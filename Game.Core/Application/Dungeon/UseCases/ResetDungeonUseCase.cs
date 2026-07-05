public class ResetDungeonUseCase : IUseCase<ResetDungeonRequest, ResetDungeonResponse>
{
    private readonly IGameSessionProvider _gs;

    public ResetDungeonUseCase(IGameSessionProvider gs)
    {
        _gs = gs;
    }

    public Result<ResetDungeonResponse> Execute(ResetDungeonRequest req)
    {
        if (_gs.Current is null)
        {
            DebugExtension.Fatal(this, "Game session is null!");
        }

        _gs.Current.ClearDungeon();

        var response = new ResetDungeonResponse();
        return Result<ResetDungeonResponse>.Success(response);
    }
}
