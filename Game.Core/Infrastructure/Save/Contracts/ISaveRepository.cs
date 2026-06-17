using Game.Core.Domain.Save;
using Game.Core.Infrastructure.Save.Snapshots;

namespace Game.Core.Infrastructure.Save.Contracts;

public interface ISaveRepository
{
    Result Save(GameSnapshot gameSnapshot);
    void Load();
    IReadOnlyCollection<SaveSlot> List();
}
