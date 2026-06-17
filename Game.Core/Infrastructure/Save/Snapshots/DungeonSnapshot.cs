using Game.Core.Domain.Exploration;

namespace Game.Core.Infrastructure.Save.Snapshots;

// co dzieje się w świecie gry
public class DungeonSnapshot
{
    public Dungeon Dungeon;

    public DungeonSnapshot(Dungeon dungeon)
    {
        Dungeon = dungeon;
    }
}
