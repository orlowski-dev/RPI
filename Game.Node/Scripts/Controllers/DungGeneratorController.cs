using Godot;

/// <summary>
/// Kontroler odpowiedzialny za integrację logiki generowania lochu z silnikiem Godot.
/// Inicjalizuje usługę generującą, pobiera warstwę kafelków i rysuje wygenerowany loch.
/// </summary>
public partial class DungGeneratorController : Node
{
    private Logger Logger => Logger.Instance;
    private GameManager GameManager => GameManager.Instance;
    private DungGeneratorService _service;
    private TileMapLayer _tileMapLayer;
    private Point _playerSpawnPointPos = new(50, 50);

    /// <summary>
    /// Wywoływane po załadowaniu sceny.
    /// Inicjalizuje serwis generujący, pobiera węzeł TileMapLayer i rysuje wygenerowane kafelki.
    /// </summary>
    public override void _Ready()
    {
        _service = new DungGeneratorService(logger: Logger);
        _tileMapLayer = GetNode<TileMapLayer>("TileMapLayer");
        var cells = _service.GenerateDungeon();

        foreach (var cell in cells)
        {
            var coords = CoordMapper.ToVector2I(cell.Key);
            var tileCoords = CoordMapper.ToVector2I(cell.Value);

            // Ustawienie komórki na TileMapLayer: (pozycja, atlas_coord, src_rect, autotile_coord)
            _tileMapLayer.SetCell(coords, 0, tileCoords, 0);
        }

        SpawnPlayer();
    }

    /// <summary>
    /// Tworzy instancję postaci gracza i kamery, dodając je do drzewa sceny.
    /// Pobiera odpowiednie sceny z <see cref="GameManager"/> na podstawie wybranej klasy postaci.
    /// </summary>
    private void SpawnPlayer()
    {
        var playerNode = GameManager.GetPlayerNode(
            GameManager.PlayerCharacter.CharacterClass.NodeName,
            CoordMapper.ToVector2I(_playerSpawnPointPos)
        );
        var cameraNode = GameManager.GetPlayerCameraNode();

        AddChild(playerNode);
        AddChild(cameraNode);
    }
}
