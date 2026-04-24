namespace Game.Core.Structs;

/// <summary>
/// Nagroda za pokonanie przeciwnika.
/// Trzyma tylko dane, bez żadnej logiki.
/// </summary>
public class EnemyReward
{
    public int Gold { get; private set; }

    public EnemyReward(int gold,)
    {
        Gold = gold;
    }
}
