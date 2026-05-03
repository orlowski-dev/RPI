using Godot;

public partial class DungGeneratorController : Node
{
    private DungGeneratorService _service = new();
    private TileMapLayer _tileMapLayer;

    public override void _Ready()
    {
        _tileMapLayer = GetNode<TileMapLayer>("TileMapLayer");
        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        var tile = _service.TilesDB.All["floor"][0];
        _tileMapLayer.SetCell(new(0, 0), 0, CoordMapper.ToVector2I(tile), 0);
    }
}
