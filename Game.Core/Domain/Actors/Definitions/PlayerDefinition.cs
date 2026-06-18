public static class PlayerDefinitions
{
    public static Dictionary<PlayerType, ActorDefinition> Values { get; } =
        new()
        {
            [PlayerType.Warrior] = new(new(140, 12, 10, 5, 2)),

            [PlayerType.Mage] = new(new(80, 18, 4, 10, 4)),

            [PlayerType.Archer] = new(new(100, 14, 6, 15, 6)),
        };
}
