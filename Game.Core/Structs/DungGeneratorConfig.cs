public struct DungGeneratorConfig
{
    public uint MinRoomSize { get; init; }
    public uint MaxRoomSize { get; init; }
    public uint TotalRooms { get; init; }
    public uint RoomOffset { get; init; }

    public DungGeneratorConfig(uint minRoomSize, uint maxRoomSize, uint totalRooms, uint roomOffset)
    {
        MinRoomSize = minRoomSize;
        MaxRoomSize = maxRoomSize;
        TotalRooms = totalRooms;
        RoomOffset = roomOffset;
    }
}
