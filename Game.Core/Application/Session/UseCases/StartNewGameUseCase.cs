using Game.Core.Application.Session.DTO;
using Game.Core.Application.Session.Requests;
using Game.Core.Application.Session.Responses;
using Game.Core.Domain.Session;

namespace Game.Core.Application.Session.UseCases;

public class StartNewGameUseCase : IUseCase<StartNewGameRequest, StartNewGameResponse>
{
    public Result<StartNewGameResponse> Execute(StartNewGameRequest req)
    {
        var gSessionF = new GameSessionFactory();
        var gSession = gSessionF.Create(new(req.Player));
        var dto = new StartNewGameResult(
            SessionId: gSession.Id,
            PlayerId: gSession.Player.Id,
            State: gSession.State
        );
        return Result<StartNewGameResponse>.Success(new(dto));
    }
}
