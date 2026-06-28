using Godot;

public partial class FollowCamera : Camera3D
{
    [Export]
    public Vector3 Offset = new Vector3(0, 6, 8);

    [Export]
    public float SmoothSpeed = 8f;

    private Node3D _target = null!;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _target = GetTree().CurrentScene.GetNode<CharacterBody3D>("Player");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (_target == null)
            return;

        Vector3 targetPosition = _target.GlobalPosition + Offset;
        GlobalPosition = GlobalPosition.Lerp(targetPosition, SmoothSpeed * (float)delta);
    }
}
