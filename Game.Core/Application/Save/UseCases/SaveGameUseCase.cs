public class SaveGameUseCase : IUseCase<SaveGameRequest, SaveGameResponse>
{
    private JsonSaveRepository _repo;
    private IGameSessionProvider _gs;

    public SaveGameUseCase(JsonSaveRepository repo, IGameSessionProvider gs)
    {
        _repo = repo;
        _gs = gs;
    }

    public Result<SaveGameResponse> Execute(SaveGameRequest req)
    {
        if (_gs.Current is null)
        {
            DebugExtension.Fatal(this, "Game session is null!");
        }

        var snapshot = new GameSnapshotAssembler().ToSnapshot(_gs.Current);
        _repo.Save(snapshot);
        return Result<SaveGameResponse>.Success(new());
    }
}
