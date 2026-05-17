/// <summary>
/// Struktura przechowująca konfigurację generatora lochu.
/// Wszystkie wartości są wyrażone w kafelkach (tiles).
/// </summary>
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

    /// <summary>
    /// Inicjalizuje konfigurację generatora.
    /// </summary>
    /// <param name="minRoomSize">Minimalny rozmiar pokoju.</param>
    /// <param name="maxRoomSize">Maksymalny rozmiar pokoju.</param>
    /// <param name="totalRooms">Liczba pokoi.</param>
    /// <param name="roomOffset">Odległość między pokojami.</param>
    /// <param name="doorSize">Rozmiar drzwi.</param>
    /// <param name="doorOffset">Offset drzwi (domyślnie doorSize + 2).</param>
    /// <param name="corridorHeight">Wysokość korytarza (domyślnie doorSize + 2).</param>
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
