using Game.Core.Application.Save.Requests;
using Game.Core.Infrastructure.Save.Contracts;
using Game.Core.Infrastructure.Save.Mapping;
using Game.Core.Infrastructure.Save.Repositories;

namespace Game.Core.Application.Save.UseCases;

public class SaveGameUseCase : IUseCase<SaveGameRequest, SaveGameResponse>
{
    private ISaveRepository _repo;

    public SaveGameUseCase()
    {
        _repo = new JsonSaveRepository();
    }

    public Result<SaveGameResponse> Execute(SaveGameRequest req)
    {
        var snapshot = new SnapshotMapper().ToSnapshot(req.GameSession);
        _repo.Save(snapshot: snapshot);
        return Result<SaveGameResponse>.Success(new());
    }
}
