/// <summary>
/// Struktra bazowa dla postaci gracza i przeciwników
/// </summary>
/// <param name="Name">Nazwa postaci</param>
/// <param name="HP">Punkty życia</param>
/// <param name="Attack">Punkty życia</param>
/// <param name="Defense">Punkty życia</param>
/// <param name="Luck">Punkty życia</param>
/// <param name="CritChance">Szansa na trafienie krytyczne (%)</param>
public abstract class BaseCharacter
{
    public string Name { get; }
    public int HP { get; }
    public int Attack { get; }
    public int Defense { get; }
    public int Luck { get; }
    public int CritChance { get; }

    protected BaseCharacter(string name, int hP, int attack, int defense, int luck, int critChance)
    {
        Name = name;
        HP = hP;
        Attack = attack;
        Defense = defense;
        Luck = luck;
        CritChance = critChance;
    }
}
