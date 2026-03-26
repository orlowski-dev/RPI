using Godot;

public partial class PlayerController : CharacterBody2D
{
    [Export]
    public int Speed { get; set; }

    [Export]
    public int RotationSpeed { get; set; }

    public override void _Ready() { }

    public override void _PhysicsProcess(double delta)
    {
        MovePlayer(ref delta);
    }

    private void MovePlayer(ref double delta)
    {
        var direction = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");

        // poruszanie
        Velocity = direction * Speed;

        //płynna rotacja
        if (direction != Vector2.Zero)
        {
            var targetRotation = direction.Angle() + Mathf.Pi / 2; // domyślnie sprite patrzy w góręę
            Rotation = Mathf.LerpAngle(Rotation, targetRotation, RotationSpeed * (float)delta);
        }

        MoveAndSlide();
    }
}
