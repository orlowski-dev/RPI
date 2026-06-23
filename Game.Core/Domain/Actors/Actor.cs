public abstract class Actor
{
    public Guid Id { get; }
    public string Name { get; }
    public int Level { get; protected set; }
    private ActorStats _stats;

    public bool IsAlive => _stats.CurrentHp > 0;
    public ActorStats Stats => _stats;

    protected Actor(string name, ActorStats stats, int? level = null, Guid? id = null)
    {
        Name = name;
        Id = id ?? Guid.NewGuid();
        Level = level ?? 1;
        _stats = stats;
        _stats = RecalculateStats();
    }

    protected virtual ActorStats GetStats(Inventory? inventory = null)
    {
        return _stats;
    }

    public void ReceiveDamage(int value)
    {
        _stats.CurrentHp = Math.Max(0, _stats.CurrentHp - value);
    }

    public virtual void Heal(int value)
    {
        _stats.CurrentHp = Math.Min(_stats.MaxHp, _stats.CurrentHp + value);
    }

    protected virtual ActorStats RecalculateStats()
    {
        return new(
            maxHp: _stats.MaxHp + (Level * _stats.MaxHp),
            attack: _stats.Attack + (Level * _stats.Attack),
            defense: _stats.Defense + (Level * _stats.Defense),
            criticalChance: _stats.CriticalChance,
            luck: _stats.Luck
        );
    }

    protected virtual void LevelUp()
    {
        Level += 1;
        _stats = RecalculateStats();
    }
}
