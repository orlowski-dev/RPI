using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class DungeonView : Control
{
    private IGameSessionProvider _gs = null!;
    private Player? _Player => _gs.Current?.Player;
    private Dungeon? _Dungeon => _gs.Current?.Dungeon;
    private InventoryView _inventoryView = null!;

    private enum Labels
    {
        Hp,
        PlayerName,
        EncounterCount,
    }

    private enum Bars
    {
        Hp,
        Exp,
    }

    private Dictionary<Labels, Label> _labels = new();
    private Dictionary<Bars, TextureProgressBar> _bars = new();
    private Button _backpackBtn = null!;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _gs = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();

        _labels[Labels.Hp] = GetNode<Label>("%PlayerHPLabel");
        _labels[Labels.PlayerName] = GetNode<Label>("%PlayerName");
        _labels[Labels.EncounterCount] = GetNode<Label>("%EncounterCount");
        _bars[Bars.Hp] = GetNode<TextureProgressBar>("%Hpbar");
        _bars[Bars.Exp] = GetNode<TextureProgressBar>("%Exp");
        _backpackBtn = GetNode<Button>("%BackpackButton");
        _inventoryView = GD.Load<PackedScene>(ScenePaths.Inventory).Instantiate<InventoryView>();
        _inventoryView.SetInvisible();
        _inventoryView.OnCloseAction = OnCloseInventoryClick;
    }

    public void InitUI()
    {
        if (_Player is null)
        {
            DebugExtension.Fatal(this, "Player is null!");
        }

        if (_Dungeon is null)
        {
            DebugExtension.Fatal(this, "Dungeon is null!");
        }

        _labels[Labels.Hp].Text = $"{_Player.Stats.CurrentHp} / {_Player.Stats.MaxHp}";
        _labels[Labels.PlayerName].Text = _Player.DisplayName;
        _labels[Labels.EncounterCount].Text =
            $"Ukończone bitwy: {_Dungeon.FinishedEncounters} / {_Dungeon.Encounters.Count}";
        _bars[Bars.Hp].MaxValue = _Player.Stats.MaxHp;
        _bars[Bars.Hp].Value = _Player.Stats.CurrentHp;

        _bars[Bars.Exp].MaxValue = _Player.ExpNextLevel;
        _bars[Bars.Exp].Value = _Player.Exp;

        _backpackBtn.Pressed += OnBackpackClick;
        GetTree().CurrentScene.GetNode<CanvasLayer>("CanvasLayer").AddChild(_inventoryView);
    }

    private void UpdateUI()
    {
        if (_Player is null)
        {
            DebugExtension.Fatal(this, "Player is null!");
        }

        if (_Dungeon is null)
        {
            DebugExtension.Fatal(this, "Dungeon is null!");
        }

        _labels[Labels.Hp].Text = $"{_Player.Stats.CurrentHp} / {_Player.Stats.MaxHp}";
        _labels[Labels.PlayerName].Text = _Player.DisplayName;
        _labels[Labels.EncounterCount].Text =
            $"Ukończone bitwy: {_Dungeon.FinishedEncounters} / {_Dungeon.Encounters.Count}";
        _bars[Bars.Hp].MaxValue = _Player.Stats.MaxHp;
        _bars[Bars.Hp].Value = _Player.Stats.CurrentHp;

        _bars[Bars.Exp].MaxValue = _Player.ExpNextLevel;
        _bars[Bars.Exp].Value = _Player.Exp;
    }

    private void OnBackpackClick()
    {
        Visible = false;
        _inventoryView.SetVisible();
    }

    private void OnCloseInventoryClick()
    {
        _inventoryView.SetInvisible();
        Visible = true;
        UpdateUI();
    }
}
