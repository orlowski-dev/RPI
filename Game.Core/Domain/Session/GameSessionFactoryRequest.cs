namespace Game.Core.Domain.Session;

public static class GameSessionFactoryRequests
{
    public record Create(PlayerFactoryRequests.Create Player);
}
