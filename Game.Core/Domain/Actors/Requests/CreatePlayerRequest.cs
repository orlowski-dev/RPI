namespace Game.Core.Domain.Actors.Requests;

public record CreatePlayerRequest(string Name, PlayerType Type);
