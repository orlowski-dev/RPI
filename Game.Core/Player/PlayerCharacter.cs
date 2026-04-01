/// <summary>
/// Postać gracza - jej statystki, umiejętności ..?
/// </summary>
/// <param name="Level">Poziom postaci gracza</param>
/// <param name="Exp">Punkty doświadczenia gracza</param>
public partial class PlayerCharacter : BaseCharacter
{
    public int Level { get; private set; }
    public int Exp { get; private set; }
    public int ExpNextLvl { get; private set; }

    // TODO: dodać później umiejętności etc tutaj

    public PlayerCharacter(
        ISignals signals,
        string name,
        int maxHp,
        int attack,
        int defense,
        int luck,
        int critChance
    )
        : base(signals, name, maxHp, attack, defense, luck, critChance)
    {
        Level = 1;
        ExpNextLvl = CalculateExpToNextLevel();
        Exp = 0;
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
    /// Zwiększa poziom postaci o 1 oraz aktualizuje wymagane doświadczenie
    /// do osiągnięcia następnego poziomu.
    /// </summary>
    /// <remarks>
    /// Metoda powinna być wywoływana po osiągnięciu wymaganego doświadczenia.
    /// </remarks>
    private void LevelUp()
    {
        Level += 1;
        ExpNextLvl = CalculateExpToNextLevel();

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
