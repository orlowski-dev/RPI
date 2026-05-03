public partial class DungGeneratorService
{
    public List<DungRoomData> Rooms = new();

    public DungGeneratorService() { }

    private DungRoomData? LastRoom => Rooms.Count > 0 ? Rooms.Last() : null;

    public void GenerateDungeon() { }
}
