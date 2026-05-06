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
                cells.Add(
                    cell,
                    _dungTiles[DungTileType.Floor][
                        _random.Next(0, _dungTiles[DungTileType.Floor].Count)
                    ]
                );
            }
        }

        return cells;
    }
}
