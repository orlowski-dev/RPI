public class LoadGameUseCase : IUseCase<LoadGameRequest, LoadGameResponse>
{
    private JsonSaveRepository _repo;

    public LoadGameUseCase(JsonSaveRepository repo)
    {
        _repo = repo;
    }

    public Result<LoadGameResponse> Execute(LoadGameRequest req)
    {
        var snap = _repo.Load(req.SnapshotId);
        if (snap.IsFailure)
        {
            DebugExtension.Fatal(this, "Cannot load game from snapshot");
        }

        var session = new GameSnapshotAssembler().Restore(snap.Value);
        return Result<LoadGameResponse>.Success(new(session));
    }
}
