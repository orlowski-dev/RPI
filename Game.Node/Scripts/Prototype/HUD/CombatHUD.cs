using System;
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
    private GameController _gc => GameController.Instance;
    private PlayerCharacter _playerStats => _gc.PlayerCharacter;
    private CombatData _combatData;
    private string _scriptName;

    public override void _Ready()
    {
        _scriptName = this.GetType().Name;

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
        InitUI();
        _combatSignals.TurnChanged += OnTurnChanged;
    }

    public override void _ExitTree()
    {
        _combatSignals.TurnChanged -= OnTurnChanged;
    }

    private void InitUI()
    {
        PlayerNameL.Text = _playerStats.Name;
        PlayerHpPB.MaxValue = _playerStats.MaxHP;
        PlayerHpPB.Value = _playerStats.HP;
        PlayerHpL.Text = $"{_playerStats.HP}/{_playerStats.MaxHP}";
    }

    private void UpdateEnemiesUI()
    {
        InitUI();
        if (_combatData.PlayerTurn)
        {
            TurnLabel.Text = "Tura gracza";
            PlayerActionsCont.Visible = true;
        }
        else
        {
            TurnLabel.Text = "Tura przeciwnika";
            PlayerActionsCont.Visible = false;
        }
        EnemyNameL.Text = _combatData.Enemies[0].Name;
        EnemyHpPB.MaxValue = _combatData.Enemies[0].MaxHP;
        EnemyHpPB.Value = _combatData.Enemies[0].HP;
        EnemyHpL.Text = $"{_combatData.Enemies[0].MaxHP}/{_combatData.Enemies[0].HP}";
    }

    private void OnTurnChanged(CombatData combatData)
    {
        _combatData = combatData;
        UpdateEnemiesUI();
    }
}
