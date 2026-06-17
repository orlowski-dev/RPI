using Game.Core.Domain.Session;

namespace Game.Core.Application.Save.Requests;

public record SaveGameRequest(GameSession GameSession);
