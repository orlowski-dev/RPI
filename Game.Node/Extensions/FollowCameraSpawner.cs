using Godot;

public partial class FollowCameraSpawner : Node
{
    private const string FollowCameraScenePath = "res://Scenes/Camera/follow_camera.tscn";

    public Camera3D GetNode()
    {
        var cameraPacked = GD.Load<PackedScene>(FollowCameraScenePath);
        return cameraPacked.Instantiate<Camera3D>();
    }
}
