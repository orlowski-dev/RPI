namespace Game.Core.Services;

/// <summary>
/// Serwis odpowiedzialny za przyznawanie nagród po walce.
/// W Obecnie tylko Gold w przyszłości itemy też
/// </summary>
public partial class RewardService
{
    /// <summary>
    /// Dodaje graczowi nagrodę za pokonanego przeciwnika.
    /// </summary>
    /// <param name="player">Gracz, który wygrał walkę.</param>
    /// <param name="reward">Nagroda przypisana do przeciwnika.</param>
    public void GiveEnemyReward(PlayerCharacter player, EnemyReward reward)
    {
        // Na tym etapie nagroda to tylko gold.
        player.AddGold(reward.Gold);
    }
}
