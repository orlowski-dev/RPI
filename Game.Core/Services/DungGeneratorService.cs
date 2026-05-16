public partial class DungGeneratorService
{
    public List<DungRoomData> Rooms = new();
    private DungGeneratorConfig _config = CoreService.DungeonGeneratorConfig;
    private Dictionary<DungTileType, List<Point>> _dungTiles = CoreService.DungeonTiles;
    private Random _random = new Random();
    private ILogger? _logger;
    private DungRoomData? LastRoom => Rooms.Count > 0 ? Rooms.Last() : null;

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

    // returns cell - tileCoords map
    public Dictionary<Point, Point> AddRoom()
    {
        // cell, tileCoords
        Dictionary<Point, Point> cells = new();
        void AddCell(Point cell, DungTileType type) =>
            cells.Add(cell, CoreService.GetRandomDungTile(type));

        var width = _random.Next((int)_config.MinRoomSize, (int)_config.MaxRoomSize);
        var height = _random.Next((int)_config.MinRoomSize, (int)_config.MaxRoomSize);
        var startPoint = GetDrawingStartPoint(newRoomHeight: height, previousRoom: LastRoom);

        var newRoom = new DungRoomData(
            id: Rooms.Count,
            topLeftCoords: startPoint,
            size: new((uint)width, (uint)height)
        );

        _logger?.Write(
            LogLevel.Info,
            "DungGeneratorService:AddRoom",
            $"Generating room width: {width}, height: {height} at ({startPoint.X}, {startPoint.Y})"
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

        newRoom.DoorCoord = doorCoord;

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
                    // AddCell(cell, DungTileType.Door);
                    AddCell(cell, DungTileType.Floor); // TODO: temp - change later
                }
                // draw floor instead of wall on the same level as prev room's door
                else if (
                    cell.X == startPoint.X
                    && LastRoom != null
                    && cell.Y == LastRoom.DoorCoord.Y
                )
                {
                    AddCell(cell, DungTileType.Floor);
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

        Rooms.Add(newRoom);

        if (newRoom.Id < _config.TotalRooms - 1)
        {
            var corridor = AddCorridor(newRoom.DoorCoord);

            foreach (var kvp in corridor)
            {
                cells[kvp.Key] = kvp.Value;
            }
        }

        return cells;
    }

    public Dictionary<Point, Point> AddCorridor(Point doorCoord)
    {
        // cell, tileCoords
        Dictionary<Point, Point> cells = new();
        void AddCell(Point cell, DungTileType type) =>
            cells.Add(cell, CoreService.GetRandomDungTile(type));

        var startX = doorCoord.X + 1;
        var startY = doorCoord.Y - (int)(_config.CorridorHeight / 2);

        _logger?.Write(
            LogLevel.Info,
            "DungGeneratorService:AddCorridor",
            $"Drawing corridor at ({startX}, {startY})"
        );

        for (var i = startX; i <= doorCoord.X + _config.RoomOffset; i++)
        {
            for (var j = startY; j < doorCoord.Y + _config.CorridorHeight - 1; j++)
            {
                var cell = new Point(i, j);

                if (cell.Y == startY || cell.Y == doorCoord.Y + _config.CorridorHeight - 2)
                {
                    AddCell(cell, DungTileType.WallTop);
                }
                else
                {
                    AddCell(cell, DungTileType.Floor);
                }
            }
        }

        return cells;
    }

    public Point GetDrawingStartPoint(int newRoomHeight, DungRoomData? previousRoom = null)
    {
        if (previousRoom == null)
            return new(0, 0);

        _logger?.Write(
            LogLevel.Info,
            "DungGeneratorService:GetDrawingStartPoint",
            $"Last room door coord ({previousRoom.DoorCoord.X}, {previousRoom.DoorCoord.Y})"
        );

        var randomY = _random.Next(
            previousRoom.DoorCoord.Y - newRoomHeight + (int)_config.DoorOffset,
            previousRoom.DoorCoord.Y - (int)_config.DoorOffset
        );

        return new Point(
            x: previousRoom.TopLeftCoords.X
                + (int)previousRoom.Size.Width
                + (int)_config.RoomOffset,
            y: randomY
        );
    }
}
