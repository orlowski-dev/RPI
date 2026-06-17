using Game.Core.Domain.Session;
using Game.Core.Infrastructure.Save.Snapshots;

namespace Game.Core.Infrastructure.Save.Mapping;

public class SnapshotMapper
{
    public GameSnapshot ToSnapshot(GameSession gameSession)
    {
        return new(sessionId: gameSession.Id, player: MapPlayer(gameSession.Player));
    }

    public void Restore()
    {
        throw new NotImplementedException();
    }

    private PlayerSnapshot MapPlayer(Player player)
    {
        return new(
            playerId: player.Id,
            name: player.Name,
            type: player.Type,
            stats: new(
                maxHp: player.Stats.MaxHp,
                currentHp: player.Stats.CurrentHp,
                attack: player.Stats.Attack,
                defense: player.Stats.Defense,
                luck: player.Stats.Luck,
                criticalChance: player.Stats.CriticalChance
            )
        );
    }
}
