using Game.Core.Domain.Exploration;
using Game.Core.Domain.Session;
using Game.Core.Infrastructure.Save.Snapshots;

namespace Game.Core.Infrastructure.Save.Mapping;

public class SnapshotMapper
{
    public GameSnapshot ToSnapshot(GameSession gameSession)
    {
        return new(
            sessionId: gameSession.Id,
            player: MapPlayer(gameSession.Player),
            dungeon: MapDungeon(gameSession.Dungeon)
        );
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

    private DungeonSnapshot? MapDungeon(Dungeon? dungeon)
    {
        if (dungeon is null)
        {
            return null;
        }

        return new(id: dungeon.Id, encounters: dungeon.Encounters);
    }
}
