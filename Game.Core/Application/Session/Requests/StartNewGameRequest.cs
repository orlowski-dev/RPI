using Game.Core.Domain.Actors.Requests;

namespace Game.Core.Application.Session.Requests;

// przesyłane z UI przy tworzeniu postaci
public record StartNewGameRequest(CreatePlayerRequest Player);
