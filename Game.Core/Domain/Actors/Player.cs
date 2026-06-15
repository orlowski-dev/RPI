namespace Game.Core.Domain.Actors;

public class Player : Actor
{
    public int Exp { get; private set; }
    public int ExpNextLevel { get; private set; }
    public int Gold { get; private set; }

    public Player(string id, ActorBaseStats baseStats)
        : base(id, baseStats)
    {
        Exp = 0;
        ExpNextLevel = 100;
        Gold = 100;
    }
}
