public partial class DungGeneratorService
{
    private List<DungRoomData> _rooms = new();
    private DungGeneratorConfig _config = CoreService.DungeonGeneratorConfig;
    private Dictionary<DungTileType, List<Point>> _dungTiles = CoreService.DungeonTiles;
    private Random _random = new Random();
    private ILogger? _logger;
    private DungRoomData? LastRoom => _rooms.Count > 0 ? _rooms.Last() : null;

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
                    AddCell(cell, DungTileType.Door);
                }
                // draw floor instead of wall on the same level as prev room's door
                else if (
                    newRoom.Id == _config.TotalRooms - 1
                    && cell.X == startPoint.X
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

        _rooms.Add(newRoom);

        return cells;
    }

    private Point GetDrawingStartPoint()
    {
        var lastRoom = LastRoom;

        if (lastRoom == null)
            return new(0, 0);

        _logger?.Write(
            LogLevel.Info,
            "DungGeneratorService:GetDrawingStartPoint",
            $"Last room door coord ({lastRoom.DoorCoord.X}, {lastRoom.DoorCoord.Y})"
        );

        return new Point(
            x: lastRoom.TopLeftCoords.X + (int)lastRoom.Size.Width + (int)_config.RoomOffset,
            y: _random.Next(
                lastRoom.DoorCoord.Y - (int)lastRoom.Size.Height + (int)_config.DoorOffset,
                lastRoom.DoorCoord.Y + (int)_config.DoorOffset
            )
        );
    }
}
