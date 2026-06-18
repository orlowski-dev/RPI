public interface IPlayerSnapshotMapper
{
    PlayerSnapshot ToSnapshot(Player player);
    Player Restore(PlayerSnapshot snapshot);
}
