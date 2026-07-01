using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class PlayerController : CharacterBody3D
{
    private enum An
    {
        Idle,
        Running,
        Attack,
    }

    private Dictionary<PlayerType, string> _animLib = new()
    {
        [PlayerType.Warrior] = "warrior_animlib",
        [PlayerType.Archer] = "archer_animlib",
        [PlayerType.Mage] = "mage_animlib",
    };
    private Dictionary<PlayerType, Dictionary<An, string>> _anims = new()
    {
        [PlayerType.Warrior] = new()
        {
            [An.Idle] = "anim_unarmed_idle_01",
            [An.Running] = "anim_running",
        },
        [PlayerType.Archer] = new()
        {
            [An.Idle] = "anim_unarmed_idle_01",
            [An.Running] = "anim_running",
            [An.Attack] = "standing_draw_arrow",
        },
        [PlayerType.Mage] = new()
        {
            [An.Idle] = "anim_unarmed_idle_01",
            [An.Running] = "anim_running",
        },
    };

    public bool ControlEnabled { get; set; } = true;

    [Export]
    public float Speed { get; set; } = 5f;

    [Export]
    public float RotationSpeed { get; set; } = 10f;

    private AnimationPlayer _animationPlayer = null!;
    private SpotLight3D _light = null!;
    private Node3D _playerNode = null!;

    private An? _currentAnimation = null;
    private Vector3 _facingDirection = Vector3.Forward;

    private IGameSessionProvider _gsProvider = null!;
    private Player _player =>
        _gsProvider.Current?.Player ?? throw new Exception("Player in session is null!");

    public override void _Ready()
    {
        _gsProvider = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
        _playerNode = GetNode<Node3D>("Model");
        _animationPlayer = GetNode<AnimationPlayer>("Model/AnimationPlayer");
        _light = GetNode<SpotLight3D>("Light");
        PlayAnimation(An.Idle);
    }

    private void PlayAnimation(An anim)
    {
        if (_currentAnimation == anim)
            return;
        _currentAnimation = anim;

        _animationPlayer.Play(_animLib[_player.Type] + "/" + _anims[_player.Type][anim]);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!ControlEnabled)
        {
            return;
        }

        Vector3 direction = GetInputDirection();

        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
            Velocity = direction * Speed;
            _facingDirection = direction;
            PlayAnimation(An.Running);
        }
        else
        {
            Velocity = Vector3.Zero;
            PlayAnimation(An.Idle);
        }

        Basis targetBasis = Basis.LookingAt(_facingDirection, Vector3.Up);
        Basis = Basis.Slerp(targetBasis, (float)(RotationSpeed * delta));

        MoveLight();
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

    private void MoveLight()
    {
        var old = _light.GlobalPosition;
        _light.Position = new Vector3(_playerNode.Position.X, old.Y, _playerNode.Position.Z);
    }

    public async Task PlayAttackAnim()
    {
        PlayAnimation(An.Attack);
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
        PlayAnimation(An.Idle);
    }
}
