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
        // _tileMapLayer.SetCell(new(0, 0), 0, tiel, 0);
    }
}
