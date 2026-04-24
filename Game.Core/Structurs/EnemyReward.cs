namespace Game.Core.Structs;

/// <summary>
/// Nagroda za pokonanie przeciwnika.
/// Trzyma tylko dane, bez żadnej logiki.
/// </summary>
public class EnemyReward
{
    public int Gold { get; private set; }
    public int Exp { get; private set; }

    public EnemyReward(int gold, int exp)
    {
        Gold = gold;
        Exp = exp;
    }
}
