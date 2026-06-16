namespace Game.Core.Domain.Actors;

public abstract class Actor
{
    public string Id { get; }

    public ActorStats Stats { get; protected set; }
    public int Level { get; protected set; }
    public bool IsAlive => Stats.CurrentHp > 0;

    protected Actor(ActorStats stats, int? level = null, string? id = null)
    {
        Id = id ?? Guid.NewGuid().ToString();
        Level = level ?? 1;
        Stats = stats;
        Stats = RecalculateStats();
    }

    public void ReceiveDamage(int value)
    {
        Stats.CurrentHp = Math.Max(0, Stats.CurrentHp - value);
    }

    public virtual void Heal(int value)
    {
        Stats.CurrentHp = Math.Min(Stats.MaxHp, Stats.CurrentHp + value);
    }

    protected virtual ActorStats RecalculateStats()
    {
        return new(
            maxHp: Stats.MaxHp + (Level * Stats.MaxHp),
            attack: Stats.Attack + (Level * Stats.Attack),
            defense: Stats.Defense + (Level * Stats.Defense),
            criticalChance: Stats.CriticalChance,
            luck: Stats.Luck
        );
    }

    protected virtual void LevelUp()
    {
        Level += 1;
        Stats = RecalculateStats();
    }
}
