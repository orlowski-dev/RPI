using Game.Core.Application.Save.Requests;
using Game.Core.Infrastructure.Save.Contracts;
using Game.Core.Infrastructure.Save.Repositories;
using Game.Core.Infrastructure.Save.Snapshots;

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
        var playerSnapshot = new PlayerSnapshot(
            playerId: req.Player.Id,
            name: req.Player.Name,
            type: req.Player.Type,
            stats: req.Player.Stats
        );
        _repo.Save(snapshot: playerSnapshot);
        return Result<SaveGameResponse>.Success(new());
    }
}
