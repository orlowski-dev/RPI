namespace Game.Core.Domain.Actors;

public class ActorStats
{
    public int CurrentHp { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int CriticalChance { get; set; }
    public int Luck { get; set; }

    public ActorStats(int currentHp, int attack, int defense, int criticalChance, int luck)
    {
        CurrentHp = currentHp;
        Attack = attack;
        Defense = defense;
        CriticalChance = criticalChance;
        Luck = luck;
    }
}
