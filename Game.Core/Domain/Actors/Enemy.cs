namespace Game.Core.Domain.Actors;

public class Enemy : Actor
{
    public int ExpReward { get; private set; }
    public int GoldReward { get; private set; }

    public Enemy(string id, ActorBaseStats baseStats)
        : base(id, baseStats)
    {
        ExpReward = 100;
        GoldReward = 50;
    }
}
