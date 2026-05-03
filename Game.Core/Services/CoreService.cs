public static partial class CoreService
{
    public static readonly DungGeneratorConfig DungeonGeneratorConfig = new(
        minRoomSize: 20,
        maxRoomSize: 40,
        totalRooms: 5
    );
    public static readonly Dictionary<DungTileType, List<Point>> DungeonTiles = new()
    {
        {
            DungTileType.WallTop,
            new List<Point>() { new(1, 0), new(2, 0), new(3, 0), new(4, 0) }
        },
        {
            DungTileType.WallBottom,
            new List<Point>() { new(1, 4), new(2, 4), new(3, 4), new(4, 4) }
        },
        {
            DungTileType.WallLeft,
            new List<Point>() { new(0, 0), new(0, 1), new(0, 2), new(0, 3) }
        },
        {
            DungTileType.WallRight,
            new List<Point>() { new(5, 0), new(5, 1), new(5, 2), new(5, 3) }
        },
        {
            DungTileType.Floor,
            new List<Point>()
            {
                new(1, 1),
                new(2, 1),
                new(3, 1),
                new(4, 1),
                new(1, 2),
                new(2, 2),
                new(3, 2),
                new(4, 2),
                new(1, 3),
                new(2, 3),
                new(3, 3),
                new(4, 3),
            }
        },
    };
}
