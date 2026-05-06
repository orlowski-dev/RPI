public partial class DungGeneratorService
{
    private List<DungRoomData> _rooms = new();
    private DungGeneratorConfig _config = CoreService.DungeonGeneratorConfig;
    private Dictionary<DungTileType, List<Point>> _dungTiles = CoreService.DungeonTiles;
    private Random _random = new Random();
    private ILogger? _logger;

    public DungGeneratorService(ILogger? logger)
    {
        _logger = logger;
    }

    private DungRoomData? LastRoom => _rooms.Count > 0 ? _rooms.Last() : null;

    public Dictionary<Point, Point> GenerateDungeon()
    {
        return AddRoom();
    }

    private Dictionary<Point, Point> AddRoom()
    {
        // cell, tileCoords
        Dictionary<Point, Point> cells = new();
        void AddCell(Point cell, DungTileType type) =>
            cells.Add(cell, CoreService.GetRandomDungTile(type));
        var width = _random.Next((int)_config.MinRoomSize, (int)_config.MaxRoomSize);
        var height = _random.Next((int)_config.MinRoomSize, (int)_config.MaxRoomSize);

        _logger?.Write(
            LogLevel.Info,
            "DungGeneratorService",
            $"Generating dungeon - w:{width}, h:{height}"
        );

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                var cell = new Point(i, j);

                // top-left
                if (cell.X == 0 && cell.Y == 0)
                {
                    AddCell(cell, DungTileType.WallTopLeft);
                }
                // top-right
                else if (cell.X == width - 1 && cell.Y == 0)
                {
                    AddCell(cell, DungTileType.WallTopRight);
                }
                // bottom left
                else if (cell.X == 0 && cell.Y == height - 1)
                {
                    AddCell(cell, DungTileType.WallBottomLeft);
                }
                // bottom-right
                else if (cell.X == width - 1 && cell.Y == height - 1)
                {
                    AddCell(cell, DungTileType.WallBottomRight);
                }
                // left
                else if (cell.X == 0)
                {
                    AddCell(cell, DungTileType.WallLeft);
                }
                // right
                else if (cell.X == width - 1)
                {
                    AddCell(cell, DungTileType.WallRight);
                }
                // top
                else if (cell.Y == 0)
                {
                    AddCell(cell, DungTileType.WallTop);
                }
                // bottom
                else if (cell.Y == height - 1)
                {
                    AddCell(cell, DungTileType.WallBottom);
                }
                // floor
                else
                {
                    AddCell(cell, DungTileType.Floor);
                }
            }
        }

        return cells;
    }
}
