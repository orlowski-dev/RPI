using Godot;
using Godot.Collections;
using Microsoft.Extensions.DependencyInjection;

public partial class CharacterCreatorScene : Node
{
    private CharacterCreatorPresenter _presenter = null!;
    private PlayerType _selectedType;
    private GetStartCharactersResponse? _data = null;

    [Export]
    Label UI_SelectedType = null!;

    [Export]
    Container UI_TypesButtons = null!;

    [Export]
    Label UI_HpLabel = null!;

    [Export]
    Label UI_AttackLabel = null!;

    [Export]
    Label UI_DefenseLabel = null!;

    [Export]
    Label UI_CritLabel = null!;

    [Export]
    Label UI_LuckLabel = null!;

    [Export]
    TextureProgressBar UI_HpPb = null!;

    [Export]
    TextureProgressBar UI_AttackPb = null!;

    [Export]
    TextureProgressBar UI_DefensePb = null!;

    [Export]
    TextureProgressBar UI_CritPb = null!;

    [Export]
    TextureProgressBar UI_LuckPb = null!;

    public override void _Ready()
    {
        _presenter = ServiceProviderHolder.Provider.GetService<CharacterCreatorPresenter>()!;

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

        GetNode<Button>(Helpers.ConcatGodotId(UI_TypesButtons, "%Warrior")).Pressed += () =>
            OnClassButtonClick(PlayerType.Warrior);

        GetNode<Button>(Helpers.ConcatGodotId(UI_TypesButtons, "%Mage")).Pressed += () =>
            OnClassButtonClick(PlayerType.Mage);

        GetNode<Button>(Helpers.ConcatGodotId(UI_TypesButtons, "%Archer")).Pressed += () =>
            OnClassButtonClick(PlayerType.Archer);
    }

    private void OnClassButtonClick(PlayerType type)
    {
        if (_data is null)
            return;
        _selectedType = type;
        UI_SelectedType.Text = _data.TypePlurals[_selectedType];
        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        if (_data is null)
            return;

        var current = _data.ActorDefinitions[_selectedType].BaseStats;

        UI_HpLabel.Text = current.MaxHp.ToString();
        UI_HpPb.Value = current.MaxHp;

        UI_AttackLabel.Text = current.Attack.ToString();
        UI_AttackPb.Value = current.Attack;

        UI_DefenseLabel.Text = current.Defense.ToString();
        UI_DefensePb.Value = current.Defense;

        UI_CritLabel.Text = current.CriticalChance + "%";
        UI_CritPb.Value = current.CriticalChance;

        UI_LuckLabel.Text = current.CriticalChance + "%";
        UI_LuckPb.Value = current.CriticalChance;
    }
}
