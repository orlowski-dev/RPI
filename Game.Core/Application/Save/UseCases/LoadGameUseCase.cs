public class LoadGameUseCase : IUseCase<LoadGameRequest, LoadGameResponse>
{
    private JsonSaveRepository _repo;
    private readonly IGameSessionProvider _gs;

    public LoadGameUseCase(JsonSaveRepository repo, IGameSessionProvider gameSessionProvider)
    {
        _repo = repo;
        _gs = gameSessionProvider;
    }

    public Result<LoadGameResponse> Execute(LoadGameRequest req)
    {
        var snap = _repo.Load(req.SnapshotId);
        if (snap.IsFailure)
        {
            DebugExtension.Fatal(this, "Cannot load game from snapshot");
        }

        var session = new GameSnapshotAssembler().Restore(snap.Value);
        _gs.Set(session);

        return Result<LoadGameResponse>.Success(new(session));
    }
}
