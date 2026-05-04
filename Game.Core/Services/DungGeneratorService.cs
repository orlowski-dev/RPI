public partial class DungGeneratorService
{
    private List<DungRoomData> _rooms = new();

    public DungGeneratorService() { }

    private DungRoomData? LastRoom => _rooms.Count > 0 ? _rooms.Last() : null;

    public void GenerateDungeon() { }

    private void AddRoom() { }
}
