namespace Game.Core.Domain.Actors;

public static class PlayerFactoryRequests
{
    public record Create(ActorStats Stats, PlayerType Type);
}
