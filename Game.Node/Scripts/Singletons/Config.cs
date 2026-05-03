public partial class Config : BaseSingleton<Config>
{
    public DungGeneratorConfig DungeonGeneratorConfig = new(
        minRoomSize: 20,
        maxRoomSize: 40,
        totalRooms: 5
    );
}
