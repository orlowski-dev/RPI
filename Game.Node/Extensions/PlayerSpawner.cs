using Godot;

public partial class PlayerSpawner : Node
{
    private const string PlayerScenePath = "res://Scenes/Player/player.scn";

    public CharacterBody3D GetNode(string playerModelPath, Vector3? position = null)
    {
        var playerNode = GD.Load<PackedScene>(PlayerScenePath).Instantiate<CharacterBody3D>();
        playerNode.Name = "Player";

        var model = GD.Load<PackedScene>(playerModelPath).Instantiate<Node3D>();
        model.Name = "Model";

        playerNode.AddChild(model);
        playerNode.Position = position ?? Vector3.Zero;

        return playerNode;
    }
}
