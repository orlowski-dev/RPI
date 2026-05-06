using Godot;

public partial class DungGeneratorController : Node
{
    private Logger Logger => Logger.Instance;
    private DungGeneratorService _service;
    private TileMapLayer _tileMapLayer;

    public override void _Ready()
    {
        _service = new DungGeneratorService(logger: Logger);
        _tileMapLayer = GetNode<TileMapLayer>("TileMapLayer");
        var cells = _service.GenerateDungeon();

        foreach (var cell in cells)
        {
            var coords = CoordMapper.ToVector2I(cell.Key);
            var tileCoords = CoordMapper.ToVector2I(cell.Value);
            _tileMapLayer.SetCell(coords, 0, tileCoords, 0);
        }
    }
}
