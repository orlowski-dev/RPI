public class SaveGameUseCase : IUseCase<SaveGameRequest, SaveGameResponse>
{
    private ISaveRepository _repo;

    public SaveGameUseCase()
    {
        _repo = new JsonSaveRepository();
    }

    public Result<SaveGameResponse> Execute(SaveGameRequest req)
    {
        var snapshot = new GameSnapshotAssembler().ToSnapshot(req.GameSession);
        _repo.Save(snapshot);
        return Result<SaveGameResponse>.Success(new());
    }
}
