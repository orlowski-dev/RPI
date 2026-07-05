using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class PlayerController : CharacterBody3D, ICharacterAnimationController
{
    private enum An
    {
        Idle,
        Running,
        Attack,
        Death,
    }

    private Dictionary<PlayerType, string> _animLib = new()
    {
        [PlayerType.Warrior] = "mixamo",
        [PlayerType.Archer] = "mixamo",
        [PlayerType.Mage] = "mixamo",
    };
    private Dictionary<PlayerType, Dictionary<An, string>> _anims = new()
    {
        [PlayerType.Warrior] = new()
        {
            [An.Idle] = "sword_and_shield_idle",
            [An.Running] = "sword_and_shield_run",
            [An.Attack] = "sword_and_shield_attack",
            [An.Death] = "sword_and_shield_death",
        },
        [PlayerType.Archer] = new()
        {
            [An.Attack] = "archer_standing_aim_recoil",
            [An.Idle] = "archer_standing_idle",
            [An.Running] = "archer_standing_run_forward",
            [An.Death] = "archer_standing_death_forward_01",
        },
        [PlayerType.Mage] = new()
        {
            [An.Idle] = "mage_standing_idle_03",
            [An.Running] = "mage_standing_run_forward",
            [An.Attack] = "mage_standing_1h_magic_attack_01",
            [An.Death] = "mage_standing_react_death_left",
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

    private Vector3 _facingDirection = Vector3.Forward;

    private IGameSessionProvider _gsProvider = null!;
    private Player _player =>
        _gsProvider.Current?.Player ?? throw new Exception("Player in session is null!");

    private bool _deathAnimStarted = false;

    public override void _Ready()
    {
        _gsProvider = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
        _playerNode = GetNode<Node3D>("Model");
        _animationPlayer = GetNode<AnimationPlayer>("Model/AnimationPlayer");
        _light = GetNode<SpotLight3D>("Light");
    }

    private string GetAnimation(An anim)
    {
        return _animLib[_player.Type] + "/" + _anims[_player.Type][anim];
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
            _animationPlayer.Play(GetAnimation(An.Running));
        }
        else
        {
            Velocity = Vector3.Zero;
            _animationPlayer.Play(GetAnimation(An.Idle));
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

    public async Task PlayAttackAnimation()
    {
        _animationPlayer.Play(GetAnimation(An.Attack));
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
        _animationPlayer.Play(GetAnimation(An.Idle));
    }

    public async Task PlayDeathAnimation()
    {
        if (_deathAnimStarted)
            return;
        _deathAnimStarted = true;

        _animationPlayer.Play(GetAnimation(An.Death));
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
    }
}
