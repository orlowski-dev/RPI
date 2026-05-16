public struct DungGeneratorConfig
{
    // sizes are measured in tiles
    public uint MinRoomSize { get; init; }
    public uint MaxRoomSize { get; init; }
    public uint TotalRooms { get; init; }
    public uint RoomOffset { get; init; }
    public uint DoorSize { get; init; }
    public uint DoorOffset { get; init; }
    public uint CorridorHeight { get; init; }

    public DungGeneratorConfig(
        uint minRoomSize,
        uint maxRoomSize,
        uint totalRooms,
        uint roomOffset,
        uint doorSize,
        uint? doorOffset = null,
        uint? corridorHeight = null
    )
    {
        MinRoomSize = minRoomSize;
        MaxRoomSize = maxRoomSize;
        TotalRooms = totalRooms;
        RoomOffset = roomOffset;
        DoorSize = doorSize;
        DoorOffset = doorOffset ?? doorSize + 2;
        CorridorHeight = corridorHeight ?? doorSize + 2;
    }
}
