public class DungTilesetMapDB
{
    public Dictionary<string, List<Point>> All { get; init; }

    public DungTilesetMapDB()
    {
        All = new()
        {
            {
                "wallTop",
                new List<Point>() { new(1, 0), new(2, 0), new(3, 0), new(4, 0) }
            },
            {
                "wallBottom",
                new List<Point>() { new(1, 4), new(2, 4), new(3, 4), new(4, 4) }
            },
            {
                "wallLeft",
                new List<Point>() { new(0, 0), new(0, 1), new(0, 2), new(0, 3) }
            },
            {
                "wallRight",
                new List<Point>() { new(5, 0), new(5, 1), new(5, 2), new(5, 3) }
            },
            {
                "floor",
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
}
