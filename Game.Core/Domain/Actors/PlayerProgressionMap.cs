using Game.Core.Domain.Actors.Definitions;

namespace Game.Core.Domain.Actors;

public static class PlayerProgressionMap
{
    public static Dictionary<PlayerType, StatsScaleValues> Values { get; } =
        new()
        {
            [PlayerType.Warrior] = new(MaxHp: 20, Attack: 3, Defense: 3),
            [PlayerType.Archer] = new(MaxHp: 15, Attack: 4, Defense: 2),
            [PlayerType.Mage] = new(MaxHp: 12, Attack: 5, Defense: 1),
        };
}
