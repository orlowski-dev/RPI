using Game.Core.Domain.Actors.Requests;

namespace Game.Core.Domain.Session.Requests;

public record CreateGameSessionRequest(CreatePlayerRequest Player);
