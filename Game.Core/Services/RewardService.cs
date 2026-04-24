using Game.Core.Data;
namespace Game.Core.Services;

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
    /// Przyznaje nagrodę na podstawie nazwy przeciwnika.
    /// </summary>
    /// <param name="player">Gracz, który wygrał walkę.</param>
    /// <param name="enemy">Pokonany przeciwnik.</param>
    public void GiveEnemyReward(PlayerCharacter player, EnemyCharacter enemy)
    {
        // Normalizujemy nazwę, żeby "Goblin" i "goblin" działały tak samo.
        var enemyName = enemy.Name.ToLower();

        if (EnemyRewardsDB.EnemyRewards.TryGetValue(enemyName, out var reward))
        {
            // Jeśli reward istnieje, dodajemy gold graczowi.
            player.AddGold(reward.Gold);
        }

        // Jeśli rewardu nie ma w bazie, na razie nic nie robimy.
    }
}
