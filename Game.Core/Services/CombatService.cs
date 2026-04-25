/// <summary>
/// Serwis odpowiedzialny za logikę walki w grze.
/// </summary>
/// <remarks>
/// Zarządza turami, obliczaniem obrażeń oraz sprawdzaniem zakończenia walki.
/// </remarks>
public partial class CombatService
{
    /// <summary>
    /// Aktualny stan walki
    /// </summary>
    public CombatState State { get; private set; }

    /// <summary>
    /// Statystyki postaci gracza
    /// </summary>
    public PlayerCharacter PlayerCharacter { get; init; }

    // public EnemyCharacter[] Enemies { get; private set; } // później
    public EnemyCharacter Enemy { get; init; }

    /// <summary>
    /// Flaga - walka zakończona
    /// </summary>
    public bool CombatEnded { get; private set; } = false;

    public CombatService(
        PlayerCharacter playerCharacter,
        EnemyCharacter enemy,
        bool playerBegins = true
    )
    {
        PlayerCharacter = playerCharacter;
        Enemy = enemy;
        State = playerBegins ? CombatState.PlayerMove : CombatState.EnemyMove;
    }

    /// <summary>
    /// Zmienia turę pomiędzy graczem a przeciwnikiem.
    /// </summary>
    public void ChangeTurn()
    {
        State = State == CombatState.PlayerMove ? CombatState.EnemyMove : CombatState.PlayerMove;
    }

    /// <summary>
    /// Wykonuje atak pomiędzy postaciami i oblicza obrażenia.
    /// </summary>
    /// <returns>(int) Zadane obrażenia</returns>
    public int Attack<A, E>(A attacker, E enemy, bool defenseAction = false, bool crit = false)
        where A : BaseCharacter
        where E : BaseCharacter
    {
        var damage = defenseAction ? attacker.Attack * .5 : attacker.Attack;
        damage -= (enemy.Defense * .5);

        if (crit)
        {
            damage *= 1.5;
        }

        enemy.TakeDamage((int)damage);

        CheckIfCombatEnded();

        return (int)damage;
    }

    /// <summary>
    /// Sprawdza czy walka została zakończona.
    /// </summary>
    private void CheckIfCombatEnded()
    {
        if (PlayerCharacter.HP == 0)
        {
            State = CombatState.EnemyWon;
            CombatEnded = true;
        }

        if (Enemy.HP == 0)
        {
            State = CombatState.PlayerWon;
            CombatEnded = true;
        }
    }
}
