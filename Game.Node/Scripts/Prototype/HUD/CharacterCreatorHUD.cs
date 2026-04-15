using Godot;
using Godot.Collections;

public partial class CharacterCreatorHUD : Node
{
	private CharacterCreatorSignals _signals => CharacterCreatorSignals.Instance;
	private CharacterCreatorData _data;
	private CharacterClass _selectedClassStats;

	[Export]
	Dictionary<string, Label> StatsLabels { get; set; } =
		new()
		{
			{ "charClassName", null },
			{ "hp", null },
			{ "attack", null },
			{ "defense", null },
			{ "crit", null },
			{ "luck", null },
		};

	[Export]
	Dictionary<string, Button> ClassesBtns { get; set; } =
		new()
		{
			{ "warrior", null },
			{ "mag", null },
			{ "archer", null },
		};

	[Export]
	TextureRect CharClassIcon { get; set; }

	[Export]
	TextEdit CharacterName { get; set; }

	[Export]
	Button StartBtn { get; set; }

	public override void _Ready()
	{
		_signals.DataSender += HandleDataSender;

		foreach (var (className, btn) in ClassesBtns)
		{
			btn.Pressed += () => OnClassBtnPressed(className);
		}

		StartBtn.Pressed += OnStartBtnPressed;
	}

	public override void _ExitTree()
	{
		_signals.DataSender -= HandleDataSender;
	}

	private void UpdateUI()
	{
		
	}

	private void HandleDataSender(CharacterCreatorData data)
	{
		_data = data;
		_data.CharacterClasses.TryGetValue(_data.SelectedClass, out _selectedClassStats);
		UpdateUI();
	}

	private void OnClassBtnPressed(string className)
	{
		_signals.EmitSetSelectedClassName(className);
	}

	private void OnStartBtnPressed()
	{
		var name = CharacterName.Text;

		if (name.Length < 3)
			return;

		// start new game
	}
}
