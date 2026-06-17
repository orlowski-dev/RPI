using Game.Core.Infrastructure.Save.Snapshots;

namespace Game.Core.Infrastructure.Save.Contracts;

public interface IPlayerSnapshotMapper
{
    PlayerSnapshot ToSnapshot(Player player);
    Player Restore(PlayerSnapshot snapshot);
}
