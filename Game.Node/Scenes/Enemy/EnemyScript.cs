using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class EnemyScript : StaticBody3D
{
	private Node3D _model = null!;
	private Label3D _label = null!;
	private Label3D _pressLabel = null!;
	private IGameSessionProvider _gameSessionProvider = null!;
	private Area3D _eventArea = null!;
	private bool _canStartCombat = false;
	public Encounter Encounter { get; set; } = null!; // ref żebym wiedział do którego encountera on należy

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
		_eventArea = GetNode<Area3D>("EventArea");

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
}
