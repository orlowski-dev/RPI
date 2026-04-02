using Godot;

/// <summary>
/// Kamera wykorzystywana w lochach.
/// </summary>
/// <remarks>
/// Automatycznie wyszukuje "Player" Node w drzewie sceny.
/// </remarks>
public partial class PlayerRunCamera : Camera2D
{
    [Export]
    public int CameraDelay { get; set; } = 10;

    private Node2D _playerNode;

    public override void _Ready()
    {
        _playerNode = GetParent().GetNode<Node2D>("Player");

        if (_playerNode == null)
        {
            GD.PrintErr("Nie można pobrać obiektu Player w drzewie sceny!");
            return;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveCamera(ref delta);
    }

    /// <summary>
    /// Śledzenie gracza i płynny ruch kamery
    /// </summary>
    /// <param name="delta">Referencja do delta</param>
    private void MoveCamera(ref double delta)
    {
        var position = Position;
        var targetPosition = _playerNode.Position;
        Position = new Vector2(
            x: Mathf.Lerp(position.X, targetPosition.X, CameraDelay * (float)delta),
            y: Mathf.Lerp(position.Y, targetPosition.Y, CameraDelay * (float)delta)
        );
    }
}
