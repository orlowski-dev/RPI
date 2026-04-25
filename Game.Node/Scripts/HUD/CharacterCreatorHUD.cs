using Godot;
using Godot.Collections;

public partial class CharacterCreatorHUD : Node
{
    private CharacterCreatorSignals CreatorSignals => CharacterCreatorSignals.Instance;
    private Signals GlobalSignals => Signals.Instance;
    private Logger Logger => Logger.Instance;
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
            { "mage", null },
            { "archer", null },
        };

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
        CreatorSignals.DataSender += HandleDataSender;

        foreach (var (className, btn) in ClassesBtns)
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
        StatsLabels["charClassName"].Text = _selectedClassStats.Name;
        StatsLabels["hp"].Text = _selectedClassStats.HpBase.ToString();
        StatsLabels["attack"].Text = _selectedClassStats.AttackBase.ToString();
        StatsLabels["defense"].Text = _selectedClassStats.DefenseBase.ToString();
        StatsLabels["crit"].Text = _selectedClassStats.CritBase.ToString() + "%";
        StatsLabels["luck"].Text = _selectedClassStats.LuckBase.ToString();
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
