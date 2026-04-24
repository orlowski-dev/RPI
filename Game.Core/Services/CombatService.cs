namespace Game.Core.Services;
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
    /// Serwis odpowiedzialny za przyznawanie nagród po walce.
    /// Na razie obsługuje jednego przeciwnika, ale później
    /// łatwo będzie go rozszerzyć o wielu przeciwników.
    /// </summary>
    public RewardService RewardService { get; }

    /// <summary>
    /// Flaga - walka zakończona
    /// </summary>
    public bool CombatEnded { get; private set; } = false;

    public CombatService(
        PlayerCharacter playerCharacter,
        EnemyCharacter enemy,
        RewardService rewardService,
        bool playerBegins = true
    )
    {
        // Zapisujemy dane gracza i przeciwnika do serwisu walki.
        PlayerCharacter = playerCharacter;
        Enemy = enemy;

        // Podpinamy serwis nagród.
        // Dzięki temu CombatService nie musi sam dodawać golda.
        RewardService = rewardService;

        // Ustawiamy, kto zaczyna walkę.
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
        // Jeśli ktoś się broni, atak jest słabszy.
        var damage = defenseAction ? attacker.Attack * .5 : attacker.Attack;

        // Obronę przeciwnika odejmujemy od siły ataku.
        damage -= (enemy.Defense * .5);

        // Jeśli trafienie jest krytyczne, zwiększamy obrażenia.
        if (crit)
        {
            damage *= 1.5;
        }

        // Zadajemy obrażenia przeciwnikowi.
        enemy.TakeDamage((int)damage);

        // Po każdym ataku sprawdzamy, czy walka się skończyła.
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
            // Gracz przegrał, więc kończymy walkę bez nagrody.
            State = CombatState.EnemyWon;
            CombatEnded = true;
        }

        if (Enemy.HP == 0)
        {
            // Gracz wygrał walkę, więc przyznajemy nagrodę
            // przez osobny serwis odpowiedzialny za rewardy.
            RewardService.GiveEnemyReward(PlayerCharacter, Enemy);

            // Dopiero po przyznaniu nagrody oznaczamy zwycięstwo.
            State = CombatState.PlayerWon;
            CombatEnded = true;
        }
    }
}
