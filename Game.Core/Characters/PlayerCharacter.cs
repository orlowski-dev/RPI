/// <summary>
/// Postać gracza - jej statystki, umiejętności ..?
/// </summary>
/// <param name="Exp">Aktualne punkty doświadczenia</param>
/// <param name="ExpNextLvl">Wymagana ilość EXP do następnego poziomu</param>
/// <param name="Luck">Szczęście</param>
public partial class PlayerCharacter : BaseCharacter
{
    public int Exp { get; private set; }
    public int ExpNextLvl { get; private set; }
    public int Luck { get; private set; }

    // TODO: dodać później umiejętności etc tutaj

    public PlayerCharacter(
        string name,
        int maxHp,
        int attack,
        int defense,
        int luck,
        int critChance,
        ISignals? signals = null,
        int level = 1
    )
        : base(name, maxHp, attack, defense, critChance, level, signals)
    {
        Exp = 0;
        ExpNextLvl = CalculateExpToNextLevel();
        Luck = luck;
    }

    /// <summary>
    /// Oblicza ilość doświadczenia wymaganą do osiągnięcia następnego poziomu.
    /// </summary>
    /// <returns>Ilość doświadczenia wymagana do następnego poziomu</returns>
    /// <remarks>
    /// wzór: 100 * level^1.4
    /// </remarks>
    private int CalculateExpToNextLevel()
    {
        return (int)Math.Floor(100 * Math.Pow(Level, 1.4));
    }

    /// <summary>
    /// Zwiększa poziom postaci oraz aktualizuje wymagane doświadczenie
    /// do osiągnięcia następnego poziomu.
    /// </summary>
    /// <param name="levels">Ilość poziomów (def: 1)</param>
    /// <remarks>
    /// Zwiększa poziom gracza aż do momentu gdy Exp < ExpNextLvl
    /// </remarks>
    public void LevelUp(int levels = 1)
    {
        while (Exp >= ExpNextLvl)
        {
            SetLevel(levels);
            ExpNextLvl = CalculateExpToNextLevel();
        }

        // TODO: Wytriggerować UI - jakieś fajerwerki czy coś..
    }

    /// <summary>
    /// Dodaje punkty doświadczenia i zwiększa Level jeśli potrzeba.
    /// </summary>
    /// <param name="amount">Ilość Exp do dodatnia</param>
    public void AddExp(int amount)
    {
        Exp += amount;

        if (Exp > ExpNextLvl)
        {
            LevelUp();
        }
    }
}
