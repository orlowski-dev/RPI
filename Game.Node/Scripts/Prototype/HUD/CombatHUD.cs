using Godot;
using Godot.Collections;

public partial class CombatHUD : Node
{
    [Export]
    Label TurnLabel { get; set; }

    [Export]
    Label PlayerNameL { get; set; }

    [Export]
    ProgressBar PlayerHpPB { get; set; }

    [Export]
    Label PlayerHpL { get; set; }

    [Export]
    Label EnemyNameL { get; set; }

    [Export]
    ProgressBar EnemyHpPB { get; set; }

    [Export]
    Label EnemyHpL { get; set; }

    [Export]
    Container PlayerActionsCont { get; set; }

    [Export]
    Dictionary<string, Button> PlayerActionsBtns { get; set; } =
        new()
        {
            { "attack", null },
            { "defense", null },
            { "useItem", null },
            { "skipTurn", null },
        };

    private CombatSignals _combatSignals => CombatSignals.Instance;
    private CombatData _combatData;
    private string _scriptName;

    public override void _Ready()
    {
        _scriptName = "(Prototype)" + this.GetType().Name;

        // sprawdza czy wszystkie przyciski ustawione w GUI
        if (PlayerActionsBtns.Values.Contains(null))
        {
            Logger.Write(
                LogLevel.Error,
                _scriptName,
                "Ustaw wszytkie przyciski dla PlayerActions!"
            );
            return;
        }

        PlayerActionsBtns["skipTurn"].Pressed += OnSkipBtnPressed;
        PlayerActionsBtns["attack"].Pressed += OnAttackBtnPressed;
        PlayerActionsBtns["defense"].Pressed += OnDefenseBtnPressed;

        _combatSignals.DataSender += OnDataReceived;
    }

    public override void _ExitTree()
    {
        _combatSignals.DataSender -= OnDataReceived;
    }

    private void InitUI() { }

    private void UpdateUI()
    {
        PlayerNameL.Text = _combatData.PlayerCharacter.Name;
        PlayerHpPB.MaxValue = _combatData.PlayerCharacter.MaxHP;
        PlayerHpPB.Value = _combatData.PlayerCharacter.HP;
        PlayerHpL.Text = $"{_combatData.PlayerCharacter.HP}/{_combatData.PlayerCharacter.MaxHP}";

        if (_combatData.State == CombatState.PlayerMove)
        {
            TurnLabel.Text = "Tura gracza";
            PlayerActionsCont.Visible = true;
        }
        else
        {
            TurnLabel.Text = "Tura przeciwnika";
            PlayerActionsCont.Visible = false;
        }
        EnemyNameL.Text = _combatData.Enemy.Name;
        EnemyHpPB.MaxValue = _combatData.Enemy.MaxHP;
        EnemyHpPB.Value = _combatData.Enemy.HP;
        EnemyHpL.Text = $"{_combatData.Enemy.HP}/{_combatData.Enemy.MaxHP}";
    }

    private void OnSkipBtnPressed()
    {
        _combatSignals.EmitSkipTurn();
    }

    private void OnAttackBtnPressed()
    {
        _combatSignals.EmitAttackAction();
    }

    private void OnDefenseBtnPressed()
    {
        _combatSignals.EmitDefenseAction();
    }

    private void OnDataReceived(CombatData combatData)
    {
        _combatData = combatData;
        UpdateUI();
    }
}
