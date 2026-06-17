using Game.Core.Domain.Save;

namespace Game.Core.Infrastructure.Save.Contracts;

public interface ISaveRepository
{
    Result Save<T>(T snapshot)
        where T : ISnapshot;
    void Load();
    IReadOnlyCollection<SaveSlot> List();
}
