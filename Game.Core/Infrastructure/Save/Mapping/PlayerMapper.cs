using Game.Core.Infrastructure.Save.Contracts;
using Game.Core.Infrastructure.Save.Snapshots;

namespace Game.Core.Infrastructure.Save.Mapping;

public class PlayerMapper : IPlayerSnapshotMapper
{
    public PlayerSnapshot ToSnapshot(Player player)
    {
        return new(
            Id: player.Id,
            Name: player.Name,
            Type: player.Type,
            Stats: player.Stats,
            Level: player.Level
        );
    }

    public Player Restore(PlayerSnapshot snapshot)
    {
        return new(
            name: snapshot.Name,
            stats: new(
                maxHp: snapshot.Stats.MaxHp,
                currentHp: snapshot.Stats.CurrentHp,
                attack: snapshot.Stats.Attack,
                defense: snapshot.Stats.Defense,
                luck: snapshot.Stats.Luck,
                criticalChance: snapshot.Stats.CriticalChance
            ),
            id: snapshot.Id,
            level: snapshot.Level,
            type: snapshot.Type
        );
    }
}
