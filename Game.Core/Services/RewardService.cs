/// <summary>
/// Serwis odpowiedzialny za przyznawanie nagród po walce.
/// Obecnie obsługuje tylko gold, ale później można tu dodać
/// exp, itemy albo reward za kilku przeciwników.
/// </summary>
public partial class RewardService
{
    /// <summary>
    /// Baza rewardów przeciwników.
    /// Dzięki temu rewardy są trzymane osobno od logiki walki i osobno od EnemyCharacter.
    /// </summary>
    public EnemyRewardsDB EnemyRewardsDB { get; }

    public RewardService(EnemyRewardsDB enemyRewardsDb)
    {
        // Wstrzykujemy bazę rewardów do serwisu.
        EnemyRewardsDB = enemyRewardsDb;
    }

    /// <summary>
    /// Dodaje graczowi nagrodę za pokonanego przeciwnika.
    /// Reward jest wyszukiwany po EnemyType.
    /// </summary>
    /// <param name="player">Gracz, który wygrał walkę.</param>
    /// <param name="enemy">Pokonany przeciwnik.</param>
    public void GiveEnemyReward(PlayerCharacter player, EnemyCharacter enemy)
    {
        // Szukamy rewardu przypisanego do typu przeciwnika.
        if (EnemyRewardsDB.EnemyRewards.TryGetValue(enemy.EnemyType, out var reward))
        {
            // Jeśli reward istnieje, dodajemy gold graczowi.
            player.AddGold(reward.Gold);
        }

        // Jeśli rewardu nie ma w bazie, na razie nic nie robimy.
    }
}
