using Game.Core.Domain.Session;

namespace Game.Core.Application.Session.DTO;

public record StartNewGameResult(Guid SessionId, Guid PlayerId, GameSessionState State);
