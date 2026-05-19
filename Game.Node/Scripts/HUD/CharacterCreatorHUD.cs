using Godot;
using Godot.Collections;

public partial class CharacterCreatorHUD : Node
{
	private CharacterCreatorSignals CreatorSignals => CharacterCreatorSignals.Instance;
	private Signals GlobalSignals => Signals.Instance;
	private Logger Logger => Logger.Instance;
	private CharacterCreatorData _data;
	private CharacterClass _selectedClassStats;

	private Dictionary<string, Label> _statsLabels;

	private Dictionary<string, Button> _classesBtns;

	[Export]
	TextureRect CharClassIcon { get; set; }

	[Export]
	TextureRect PreviewTR { get; set; }

	[Export]
	TextEdit CharacterName { get; set; }

	[Export]
	Button StartBtn { get; set; }

	public override void _Ready()
	{
		_statsLabels = new()
		{
			{ "charClassName", GetNode<Label>("%characterClassNameL") },
			{ "hp", GetNode<Label>("%hpLabel") },
			{ "attack", GetNode<Label>("%attackLabel") },
			{ "defense", GetNode<Label>("%defenseLabel") },
			{ "crit", GetNode<Label>("%critLabel") },
			{ "luck", GetNode<Label>("%luckLabel") },
		};
		_classesBtns = new()
		{
			{ "warrior", GetNode<Button>("%warriorBtn") },
			{ "mage", GetNode<Button>("%magBtn") },
			{ "archer", GetNode<Button>("%archerBtn") },
		};

		CreatorSignals.DataSender += HandleDataSender;

		foreach (var (className, btn) in _classesBtns)
		{
			btn.Pressed += () => OnClassBtnPressed(className);
		}

		StartBtn.Pressed += OnStartBtnPressed;
	}

	public override void _ExitTree()
	{
		CreatorSignals.DataSender -= HandleDataSender;
	}

	private void UpdateUI()
	{
		_statsLabels["charClassName"].Text = _selectedClassStats.Name;
		_statsLabels["hp"].Text = _selectedClassStats.HpBase.ToString();
		_statsLabels["attack"].Text = _selectedClassStats.AttackBase.ToString();
		_statsLabels["defense"].Text = _selectedClassStats.DefenseBase.ToString();
		_statsLabels["crit"].Text = _selectedClassStats.CritBase.ToString() + "%";
		_statsLabels["luck"].Text = _selectedClassStats.LuckBase.ToString();
		CharClassIcon.Texture = GD.Load<Texture2D>(
			"res://Assets/Icons/" + _selectedClassStats.ClassIconName
		);
		PreviewTR.Texture = GD.Load<Texture2D>(_selectedClassStats.PreviewSpritePath);
	}

	private void HandleDataSender(CharacterCreatorData data)
	{
		_data = data;
		_data.CharacterClasses.TryGetValue(_data.SelectedClass, out _selectedClassStats);
		UpdateUI();
	}

	private void OnClassBtnPressed(string className)
	{
		CreatorSignals.EmitSetSelectedClassName(className);
	}

	private void OnStartBtnPressed()
	{
		var name = CharacterName.Text;

		if (name.Length < 3)
			return;

		var player = new PlayerCharacter(
			name: name,
			maxHp: _selectedClassStats.HpBase,
			defense: _selectedClassStats.DefenseBase,
			attack: _selectedClassStats.AttackBase,
			luck: _selectedClassStats.LuckBase,
			critChance: _selectedClassStats.CritBase,
			characterClass: _selectedClassStats,
			signals: GlobalSignals,
			logger: Logger
		);

		// start new game
		GlobalSignals.EmitGameStateChanged(
			new GameManagerData(gameState: GameState.City, playerCharacter: player)
		);
	}
}
