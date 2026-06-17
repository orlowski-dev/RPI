namespace Game.Core.Domain.Actors.Requests;

public record CreatePlayerRequest(string Name, ActorStats Stats, PlayerType Type);
