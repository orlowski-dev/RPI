public struct DungGeneratorConfig
{
    public uint MinRoomSize { get; init; }
    public uint MaxRoomSize { get; init; }
    public uint TotalRooms { get; init; }

    public DungGeneratorConfig(uint minRoomSize, uint maxRoomSize, uint totalRooms)
    {
        MinRoomSize = minRoomSize;
        MaxRoomSize = maxRoomSize;
        TotalRooms = totalRooms;
    }
}
