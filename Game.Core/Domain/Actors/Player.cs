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
}
