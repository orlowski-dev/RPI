using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class ArenaViewScript : Control
{
	private readonly string EnemyStatsScene = "res://Scenes/Arena/enemy_stats.tscn";

	private enum Bt
	{
		Attack,
	}

	private enum Lb
	{
		PlayerHP,
	}

	private enum Pb
	{
		PlayerHP,
	}

	private enum Ct
	{
		ActionButtons,
		EnemyStats,
	}

	private Dictionary<Bt, Button> _buttons = new();
	private Dictionary<Lb, Label> _labels = new();
	private Dictionary<Pb, TextureProgressBar> _bars = new();
	private Dictionary<Ct, Container> _containers = new();
	private Dictionary<Enemy, EnemyStatsListItem> _enemyStats = new();

	private IGameSessionProvider _gsProvider = null!;
	private ArenaPresenter _arenaPresenter = null!;
	private ArenaScene? _arenaScene;

	private CombatSession CombatSession =>
		_gsProvider.Current?.CombatSession ?? throw new Exception("Combat session not set!");
	private Player Player => CombatSession.Player;
	private IReadOnlyList<Enemy> Enemies => CombatSession.AllEnemies;

	public override void _Ready()
	{
		_gsProvider = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
		_arenaPresenter = ServiceProviderHolder.Provider.GetRequiredService<ArenaPresenter>();

		GetNodes();
	}

	public void Init(ArenaScene arenaScene)
	{
		_arenaScene = arenaScene;
		InitUI();
		UpdateUI();
	}

	public void UpdateUI()
	{
		_containers[Ct.ActionButtons].Visible =
			CombatSession.ActiveParticipant is Player ? true : false;

		_bars[Pb.PlayerHP].Value = Player.Stats.CurrentHp;

		_labels[Lb.PlayerHP].Text = $"{Player.Stats.CurrentHp} / {Player.Stats.MaxHp}";

		foreach (var enemy in Enemies)
		{
			var current = _enemyStats[enemy];
			current.CurrentEnemyHP = enemy.Stats.CurrentHp;
		}
	}

	private void InitUI()
	{
		_bars[Pb.PlayerHP].MaxValue = Player.Stats.MaxHp;
		foreach (var enemy in CombatSession.AllEnemies)
		{
			var scene = GD.Load<PackedScene>(EnemyStatsScene).Instantiate<EnemyStatsListItem>();
			_containers[Ct.EnemyStats].AddChild(scene);
			scene.Init(name: enemy.Name, maxHp: enemy.Stats.MaxHp);
			_enemyStats[enemy] = scene;
		}
	}

	private void GetNodes()
	{
		_buttons[Bt.Attack] = GetNode<Button>("%AttackButton");
		_buttons[Bt.Attack].Pressed += OnAttackButtonClick;

		_containers[Ct.ActionButtons] = GetNode<Container>("%ActionButtons");
		_bars[Pb.PlayerHP] = GetNode<TextureProgressBar>("%PlayerHealthBar");
		_labels[Lb.PlayerHP] = GetNode<Label>("%PlayerHPLabel");
		_containers[Ct.EnemyStats] = GetNode<Container>("%EnemyStatsContainer");
	}

	private async void OnAttackButtonClick()
	{
		// var res = _arenaPresenter.OnPlayerAttackAction();
		// GD.Print(_gsProvider.Current?.CombatSession?.GetInfo() ?? "Combat session does not exist!");

		// await ToSignal(GetTree().CreateTimer(ActionDelay), Godot.Timer.SignalName.Timeout);

		if (_gsProvider.Current?.CombatSession?.Target is null)
		{
			return;
		}

		_arenaPresenter.OnPlayerAttackAction();
		_arenaScene?.StartCombatSteps();
	}

	public void SetArenaScene(ArenaScene arenaScene)
	{
		_arenaScene = arenaScene;
	}
}
