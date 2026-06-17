using Game.Core.Domain.Exploration;
using Game.Core.Infrastructure.Save.Snapshots;

namespace Game.Core.Infrastructure.Save.Contracts;

public interface IDungeonSnapshot
{
    DungeonSnapshot ToSnapshot(Dungeon dungeon);
    Dungeon Restore(DungeonSnapshot worldSnapshot);
}
