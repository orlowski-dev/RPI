using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class EnemyScript : StaticBody3D, ICharacterAnimationController
{
	private Node3D _model = null!;
	private Label3D _label = null!;
	private Label3D _pressLabel = null!;
	private IGameSessionProvider _gameSessionProvider = null!;
	private Area3D _eventArea = null!;
	private bool _canStartCombat = false;
	private EnemyAnimations _anims = new EnemyAnimations();
	private AnimationPlayer _animPlayer = null!;
	private CollisionShape3D _collision = null!;

	public Encounter Encounter { get; set; } = null!; // ref żebym wiedział do którego encountera on należy
	public Enemy Enemy { get; set; } = null!;

	public string Label
	{
		get => _label.Text;
		set => _label.Text = value;
	}

	public override void _Ready()
	{
		_gameSessionProvider =
			ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
		_label = GetNode<Label3D>("%Label");
		_pressLabel = GetNode<Label3D>("%PressLabel");
		_eventArea = GetNode<Area3D>("%EventArea");
		_animPlayer = GetNode<AnimationPlayer>("Model/AnimationPlayer");
		_collision = GetNode<CollisionShape3D>("Collision");

		if (_animPlayer is null)
		{
			throw new Exception("_animPlayer is null!");
		}

		if (Enemy is not null && !Enemy.IsAlive)
		{
			_canStartCombat = false;
			GD.Print("Enemy is dead.");
			_animPlayer.Play(_anims.GetAnimation(Enemy.Type, EnemyAnimations.Anim.Death));
			_animPlayer.Seek(_animPlayer.CurrentAnimationLength, true);
			_animPlayer.Pause();
			_pressLabel.Visible = false;
			_eventArea.Monitoring = false; // wyłącz kolizje
			return;
		}

		_eventArea.BodyEntered += OnBodyEntered;
		_eventArea.BodyExited += OnBodyExited;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsKeyPressed(keycode: Key.E) && _canStartCombat)
		{
			GD.Print("Entering combat arena..");
			_gameSessionProvider.Current!.PendingEncounter = Encounter;
			GetTree().CallDeferred("change_scene_to_file", ScenePaths.Arena);
			_canStartCombat = false;
			return;
		}
	}

	private void OnBodyEntered(Node3D body)
	{
		if (body is PlayerController)
		{
			_pressLabel.Visible = true;
			_canStartCombat = true;
		}
	}

	private void OnBodyExited(Node3D body)
	{
		if (body is PlayerController)
		{
			_pressLabel.Visible = false;
			_canStartCombat = false;
		}
	}

	public async Task PlayAttackAnimation()
	{
		_animPlayer.Play(_anims.GetAnimation(Enemy.Type, EnemyAnimations.Anim.Atack));
		await ToSignal(_animPlayer, AnimationPlayer.SignalName.AnimationFinished);
		_animPlayer.Play(_anims.GetAnimation(Enemy.Type, EnemyAnimations.Anim.Idle));
	}

	public async Task PlayDeathAnimation()
	{
		_animPlayer.Play(_anims.GetAnimation(Enemy.Type, EnemyAnimations.Anim.Death));
	}

	public void DisableCollisions()
	{
		_collision.Disabled = true;
	}
}
