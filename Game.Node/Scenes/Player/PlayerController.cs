using Godot;

public partial class PlayerController : CharacterBody3D
{
    private const string AnimLib = "archer_animlib";

    private enum An
    {
        Idle,
        Running,
    }

    [Export]
    public float Speed { get; set; } = 5f;

    [Export]
    public float RotationSpeed { get; set; } = 10f; // im wyżej, tym szybszy obrót

    private AnimationPlayer _animationPlayer = null!;
    private Dictionary<An, string> _anims = new()
    {
        { An.Running, "anim_running" },
        { An.Idle, "anim_unarmed_idle_01" },
    };
    private string _currentAnimation = string.Empty;
    private Vector3 _facingDirection = Vector3.Forward; // zapamiętany kierunek

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("Model/AnimationPlayer");
        PlayAnimation(_anims[An.Idle]);
    }

    private void PlayAnimation(string animName)
    {
        if (_currentAnimation == animName)
            return;
        _currentAnimation = animName;
        _animationPlayer.Play(AnimLib + "/" + animName);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 direction = GetInputDirection();

        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
            Velocity = direction * Speed;
            _facingDirection = direction;
            PlayAnimation(_anims[An.Running]);
        }
        else
        {
            Velocity = Vector3.Zero;
            PlayAnimation(_anims[An.Idle]);
        }

        Basis targetBasis = Basis.LookingAt(_facingDirection, Vector3.Up);
        Basis = Basis.Slerp(targetBasis, (float)(RotationSpeed * delta));

        MoveAndSlide();
    }

    private Vector3 GetInputDirection()
    {
        Vector3 dir = Vector3.Zero;
        if (Input.IsActionPressed("moveUp") || Input.IsKeyPressed(Key.W))
            dir.Z -= 1;
        if (Input.IsActionPressed("moveDown") || Input.IsKeyPressed(Key.S))
            dir.Z += 1;
        if (Input.IsActionPressed("moveLeft") || Input.IsKeyPressed(Key.A))
            dir.X -= 1;
        if (Input.IsActionPressed("moveRight") || Input.IsKeyPressed(Key.D))
            dir.X += 1;
        return dir;
    }
}
