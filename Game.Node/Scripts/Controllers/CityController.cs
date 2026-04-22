using Godot;

public partial class CityController : Node
{
    private GameManager GameManager => GameManager.Instance;
    private CitySignals CitySignals => CitySignals.Instance;
    private Node2D _playerSpawnPoint;

    public override void _Ready()
    {
        _playerSpawnPoint = GetNode<Node2D>("PlayerSpawnPoint");
        var playerScene = GD.Load<PackedScene>(GameManager.PlayerCharacter.CharacterClass.NodeName);
        var playerNode = playerScene.Instantiate();
        _playerSpawnPoint.AddChild(playerNode);

        CitySignals.EmitDataSender(new CityHudData(GameManager.PlayerCharacter));
    }
}
