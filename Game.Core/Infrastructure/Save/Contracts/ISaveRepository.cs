using Game.Core.Infrastructure.Save.Snapshots;

namespace Game.Core.Infrastructure.Save.Contracts;

public interface ISaveRepository
{
    Result Save(GameSnapshot gameSnapshot);
    Result<GameSnapshot> Load(string snapshotId);
    Result<GameSnapshot> Load(Guid snapshotId);
    IReadOnlyCollection<GameSnapshot> List();
}
