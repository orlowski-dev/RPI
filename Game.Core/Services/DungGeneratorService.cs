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

    public Dictionary<Point, Point> GenerateDungeon()
    {
        Dictionary<Point, Point> cells = new();

        for (var i = 0; i < _config.TotalRooms; i++)
        {
            var newRoom = AddRoom();
            foreach (var kvp in newRoom)
            {
                cells[kvp.Key] = kvp.Value;
            }
        }

        return cells;
    }

    private Dictionary<Point, Point> AddRoom()
    {
        // cell, tileCoords
        Dictionary<Point, Point> cells = new();
        void AddCell(Point cell, DungTileType type) =>
            cells.Add(cell, CoreService.GetRandomDungTile(type));

        var width = _random.Next((int)_config.MinRoomSize, (int)_config.MaxRoomSize);
        var height = _random.Next((int)_config.MinRoomSize, (int)_config.MaxRoomSize);
        var startPoint = GetDrawingStartPoint();

        var newRoom = new DungRoomData(
            id: _rooms.Count,
            topLeftCoords: startPoint,
            size: new((uint)width, (uint)height)
        );
        _rooms.Add(newRoom);

        _logger?.Write(
            LogLevel.Info,
            "DungGeneratorService",
            $"Generating room w:{width}, h:{height} at {startPoint.X}x{startPoint.Y}"
        );

        var targetW = startPoint.X + width;
        var targetH = startPoint.Y + height;

        var doorCoord = new Point(
            x: targetW - 1,
            y: _random.Next(
                startPoint.Y + (int)_config.DoorOffset,
                targetH - (int)_config.DoorOffset
            )
        );

        for (var i = startPoint.X; i < targetW; i++)
        {
            for (var j = startPoint.Y; j < targetH; j++)
            {
                var cell = new Point(i, j);

                // door, except last room
                if (
                    newRoom.Id < _config.TotalRooms - 1
                    && cell.X == doorCoord.X
                    && cell.Y == doorCoord.Y
                )
                {
                    AddCell(cell, DungTileType.Door);
                }
                // top-left
                else if (cell.X == startPoint.X && cell.Y == startPoint.Y)
                {
                    AddCell(cell, DungTileType.WallTopLeft);
                }
                // top-right
                else if (cell.X == targetW - 1 && cell.Y == startPoint.Y)
                {
                    AddCell(cell, DungTileType.WallTopRight);
                }
                // bottom left
                else if (cell.X == startPoint.X && cell.Y == targetH - 1)
                {
                    AddCell(cell, DungTileType.WallBottomLeft);
                }
                // bottom-right
                else if (cell.X == targetW - 1 && cell.Y == targetH - 1)
                {
                    AddCell(cell, DungTileType.WallBottomRight);
                }
                // left
                else if (cell.X == startPoint.X)
                {
                    AddCell(cell, DungTileType.WallLeft);
                }
                // right
                else if (cell.X == targetW - 1)
                {
                    AddCell(cell, DungTileType.WallRight);
                }
                // top
                else if (cell.Y == startPoint.Y)
                {
                    AddCell(cell, DungTileType.WallTop);
                }
                // bottom
                else if (cell.Y == targetH - 1)
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

    private Point GetDrawingStartPoint()
    {
        if (_rooms.Count == 0)
            return new(0, 0);

        var lastRoom = _rooms[_rooms.Count - 1];
        return new(
            lastRoom.TopLeftCoords.X + (int)lastRoom.Size.Width + (int)_config.RoomOffset,
            lastRoom.TopLeftCoords.Y
        );
    }
}
