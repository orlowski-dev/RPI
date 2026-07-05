public interface ISaveRepository
{
    Result Save(GameSnapshot gameSnapshot);
    Result<GameSnapshot> Load(string snapshotId);
    Result<GameSnapshot> Load(Guid snapshotId);
    IReadOnlyList<GameSnapshot> List();
}
