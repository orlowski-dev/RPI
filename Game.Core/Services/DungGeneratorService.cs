public partial class DungGeneratorService
{
    public List<DungRoomData> Rooms = new();
    public DungTilesetMapDB TilesDB = new();

    public DungGeneratorService() { }

    private DungRoomData? LastRoom => Rooms.Count > 0 ? Rooms.Last() : null;
}
