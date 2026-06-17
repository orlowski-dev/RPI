using Game.Core.Infrastructure.Save.Contracts;
using Game.Core.Infrastructure.Save.Snapshots;

namespace Game.Core.Infrastructure.Save.Mapping;

public class PlayerMapper : IPlayerSnapshotMapper
{
    public PlayerSnapshot ToSnapshot(Player player)
    {
        return new(
            playerId: player.Id,
            name: player.Name,
            type: player.Type,
            stats: player.Stats,
            level: player.Level
        );
    }

    public Player Restore(PlayerSnapshot snapshot)
    {
        return new(
            name: snapshot.Name,
            stats: snapshot.Stats,
            id: snapshot.Id,
            level: snapshot.Level,
            type: snapshot.Type
        );
    }
}
