namespace Game.Core.Domain.Actors;

public class Player : Actor
{
    public PlayerType Type { get; private set; }
    public int Exp { get; private set; }
    public int ExpNextLevel { get; private set; }
    public int Gold { get; private set; }

    public Player(ActorStats stats, PlayerType type, string? id = null)
        : base(stats: stats, id: id)
    {
        Type = type;
        Exp = 0;
        ExpNextLevel = CalculateExpNextLevel();
        Gold = 100;
    }

    public void AddExperience(int amount)
    {
        if (amount <= 0)
            return;

        Exp += amount;

        while (Exp >= ExpNextLevel)
        {
            Exp -= ExpNextLevel;
            LevelUp();
            ExpNextLevel = CalculateExpNextLevel();
        }
    }

    public void AddGold(int amount)
    {
        if (amount <= 0)
            return;

        Gold += amount;
    }

    private int CalculateExpNextLevel()
    {
        return (int)Math.Floor(100 * Math.Pow(Level, 1.5));
    }

    protected override ActorStats RecalculateStats()
    {
        var map = PlayerProgressionMap.Values;

        return new(
            maxHp: Stats.MaxHp + (Level * Stats.MaxHp) + map[Type].MaxHp,
            attack: Stats.Attack + (Level * Stats.Attack) + map[Type].Attack,
            defense: Stats.Defense + (Level * Stats.Defense) + map[Type].Defense,
            criticalChance: Stats.CriticalChance,
            luck: Stats.Luck
        );
    }
}
