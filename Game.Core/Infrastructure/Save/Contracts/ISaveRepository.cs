using Game.Core.Domain.Save;

namespace Game.Core.Infrastructure.Save.Contracts;

public interface ISaveRepository
{
    Result Save(ISnapshot snapshot);
    void Load();
    IReadOnlyCollection<SaveSlot> List();
}
