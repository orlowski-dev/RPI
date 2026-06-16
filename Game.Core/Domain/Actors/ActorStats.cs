namespace Game.Core.Domain.Actors;

public class ActorStats
{
    public int MaxHp { get; set; }
    public int CurrentHp { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int CriticalChance { get; set; }
    public int Luck { get; set; }

    public ActorStats(int maxHp, int attack, int defense, int criticalChance, int luck)
    {
        MaxHp = maxHp;
        CurrentHp = maxHp;
        Attack = attack;
        Defense = defense;
        CriticalChance = criticalChance;
        Luck = luck;
    }
}
