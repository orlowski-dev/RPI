namespace Game.Core.Domain.Actors;

public abstract class Actor
{
    public string Id { get; }
    public ActorBaseStats BaseStats { get; }
    public ActorStats Stats { get; private set; }
    public int Level { get; }
    public bool IsAlive => Stats.CurrentHp > 0;

    protected Actor(string id, ActorBaseStats baseStats, int? level = null)
    {
        Id = id;
        BaseStats = baseStats;
        Level = level ?? 1;
        Stats = new(
            currentHp: BaseStats.MaxHp * Level,
            attack: BaseStats.Attack * Level,
            defense: BaseStats.Defense * Level,
            criticalChance: BaseStats.CriticalChance,
            luck: BaseStats.Luck
        );
    }

    public void ReceiveDamage(int value)
    {
        Stats.CurrentHp = Math.Max(0, Stats.CurrentHp - value);
    }

    public virtual void Heal(int value)
    {
        Stats.CurrentHp = Math.Min(Stats.CurrentHp, Stats.CurrentHp + value);
    }
}
