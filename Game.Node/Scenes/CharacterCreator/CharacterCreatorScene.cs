using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class CharacterCreatorScene : Node
{
    private enum Btn
    {
        Warrior,
        Mage,
        Archer,
        Start,
    }

    private enum Lbl
    {
        WarriorDesc,
        MageDesc,
        ArcherDesc,
        Hp,
        Attack,
        Defense,
        Crit,
        Luck,
        SelectedType,
    }

    private enum Pb
    {
        Hp,
        Attack,
        Defense,
        Crit,
        Luck,
    }

    private enum Tx
    {
        ClassPreview,
    }

    private Dictionary<Btn, Button> _buttons = new();
    private Dictionary<Lbl, Label> _labels = new();
    private Dictionary<Pb, TextureProgressBar> _progressBars = new();
    private Dictionary<Tx, TextureRect> _textures = new();
    private LineEdit _playerName = null!;

    // private GameSession _session = null!;

    private CharacterCreatorPresenter _presenter = null!;
    private PlayerType _selectedType;
    private GetStartCharactersResponse? _data = null;
    private Node3D _playerPreview = null!;

    public override void _Ready()
    {
        _presenter = ServiceProviderHolder.Provider.GetRequiredService<CharacterCreatorPresenter>();
        InitUI();
        LinkUI();

        if (_presenter is null)
        {
            DebugExtension.Fatal(this, "Presenter is null.");
        }

        if (_presenter.OnViewReady().Error is not null)
        {
            DebugExtension.Fatal(this, "Cannot get response from use case");
        }

        if (_presenter.OnViewReady().Data is null)
        {
            DebugExtension.Fatal(this, "Cannot get data from Presenter");
        }

        _data = _presenter.OnViewReady().Data;

        UpdateStatsUI();
    }

    private void InitUI()
    {
        _buttons[Btn.Warrior] = GetTree().CurrentScene.GetNode<Button>("%Btn_Warrior");
        _buttons[Btn.Archer] = GetTree().CurrentScene.GetNode<Button>("%Btn_Archer");
        _buttons[Btn.Mage] = GetTree().CurrentScene.GetNode<Button>("%Btn_Mage");
        _buttons[Btn.Start] = GetTree().CurrentScene.GetNode<Button>("%Btn_StartGame");

        _labels[Lbl.WarriorDesc] = GetTree().CurrentScene.GetNode<Label>("%L_Warrior");
        _labels[Lbl.ArcherDesc] = GetTree().CurrentScene.GetNode<Label>("%L_Archer");
        _labels[Lbl.MageDesc] = GetTree().CurrentScene.GetNode<Label>("%L_Mage");

        _labels[Lbl.Hp] = GetTree().CurrentScene.GetNode<Label>("%L_HP");
        _labels[Lbl.Attack] = GetTree().CurrentScene.GetNode<Label>("%L_Attack");
        _labels[Lbl.Defense] = GetTree().CurrentScene.GetNode<Label>("%L_Defense");
        _labels[Lbl.Crit] = GetTree().CurrentScene.GetNode<Label>("%L_Crit");
        _labels[Lbl.Luck] = GetTree().CurrentScene.GetNode<Label>("%L_Luck");
        _labels[Lbl.SelectedType] = GetTree().CurrentScene.GetNode<Label>("%L_SelectedType");

        _progressBars[Pb.Hp] = GetTree().CurrentScene.GetNode<TextureProgressBar>("%Pb_HP");
        _progressBars[Pb.Attack] = GetTree().CurrentScene.GetNode<TextureProgressBar>("%Pb_Attack");
        _progressBars[Pb.Defense] = GetTree()
            .CurrentScene.GetNode<TextureProgressBar>("%Pb_Defense");
        _progressBars[Pb.Crit] = GetTree().CurrentScene.GetNode<TextureProgressBar>("%Pb_Crit");
        _progressBars[Pb.Luck] = GetTree().CurrentScene.GetNode<TextureProgressBar>("%Pb_Luck");

        _textures[Tx.ClassPreview] = GetTree()
            .CurrentScene.GetNode<TextureRect>("%Tx_ClassPreview");

        _playerName = GetTree().CurrentScene.GetNode<LineEdit>("%Le_PlayerName");

        _playerPreview = GetTree().CurrentScene.GetNode<Node3D>("%PlayerPreview");
    }

    private void LinkUI()
    {
        _buttons[Btn.Warrior].Pressed += () => OnClassButtonClick(PlayerType.Warrior);
        _buttons[Btn.Archer].Pressed += () => OnClassButtonClick(PlayerType.Archer);
        _buttons[Btn.Mage].Pressed += () => OnClassButtonClick(PlayerType.Mage);
        _buttons[Btn.Start].Pressed += OnStartClick;
    }

    private void OnClassButtonClick(PlayerType type)
    {
        if (_data is null)
            return;
        _selectedType = type;
        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        if (_data is null)
            return;

        var current = _data.PlayerDefinitions[_selectedType];

        if (_selectedType == PlayerType.Warrior)
        {
            _labels[Lbl.WarriorDesc].Visible = true;
            _labels[Lbl.WarriorDesc].Text = current.TypeDescription;
            _labels[Lbl.ArcherDesc].Visible = false;
            _labels[Lbl.MageDesc].Visible = false;
        }

        if (_selectedType == PlayerType.Archer)
        {
            _labels[Lbl.WarriorDesc].Visible = false;
            _labels[Lbl.ArcherDesc].Visible = true;
            _labels[Lbl.ArcherDesc].Text = current.TypeDescription;
            _labels[Lbl.MageDesc].Visible = false;
        }

        if (_selectedType == PlayerType.Mage)
        {
            _labels[Lbl.WarriorDesc].Visible = false;
            _labels[Lbl.ArcherDesc].Visible = false;
            _labels[Lbl.MageDesc].Visible = true;
            _labels[Lbl.MageDesc].Text = current.TypeDescription;
        }

        _labels[Lbl.Hp].Text = current.ActorStats.MaxHp.ToString();
        _labels[Lbl.Attack].Text = current.ActorStats.Attack.ToString();
        _labels[Lbl.Defense].Text = current.ActorStats.Defense.ToString();
        _labels[Lbl.Crit].Text = current.ActorStats.CriticalChance + "%";
        _labels[Lbl.Luck].Text = current.ActorStats.Luck + "%";

        _progressBars[Pb.Hp].Value = current.ActorStats.MaxHp;
        _progressBars[Pb.Attack].Value = current.ActorStats.Attack;
        _progressBars[Pb.Defense].Value = current.ActorStats.Defense;
        _progressBars[Pb.Crit].Value = current.ActorStats.CriticalChance;
        _progressBars[Pb.Luck].Value = current.ActorStats.Luck;

        _labels[Lbl.SelectedType].Text = current.TypePlural;

        var image = GD.Load<Texture2D>(current.PreviewImage);
        _textures[Tx.ClassPreview].Texture = image;

        SpawnPlayerPreview();
    }

    private void OnStartClick()
    {
        if (_playerName.Text.Length < 3)
        {
            return;
        }

        var vm = _presenter.OnNewGame(playerName: _playerName.Text, playerType: _selectedType);

        if (vm.Error is not null || vm.GameSession is null)
        {
            return;
        }

        GetTree().CallDeferred("change_scene_to_file", ScenePaths.City);
    }

    private void SpawnPlayerPreview()
    {
        foreach (var child in _playerPreview.GetChildren())
        {
            child.QueueFree();
        }

        var playerScene = GD.Load<PackedScene>(PlayerDefinitions.Values[_selectedType].NodePath);
        var playerNode = playerScene.Instantiate<Node3D>();
        playerNode.Position = Vector3.Zero;
        _playerPreview.AddChild(playerNode);
    }
}
