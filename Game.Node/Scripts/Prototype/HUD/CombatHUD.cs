using System;
using Godot;

public partial class CombatHUD : Node
{
    [Export]
    Button EndTurnBtn { get; set; }

    [Export]
    Label TurnLabel { get; set; }

    [Export]
    Label PlayerNameL;

    [Export]
    ProgressBar PlayerHpPB;

    [Export]
    Label PlayerHpL;

    [Export]
    Label EnemyNameL;

    [Export]
    ProgressBar EnemyHpPB;

    [Export]
    Label EnemyHpL;

    private CombatSignals _combatSignals => CombatSignals.Instance;
    private GameController _gc => GameController.Instance;
    private PlayerCharacter _playerStats => _gc.PlayerCharacter;
    private CombatData _combatData;

    public override void _Ready()
    {
        UpdatePlayerUI();
        EndTurnBtn.Pressed += OnEndTurnBtnPressed;
        _combatSignals.TurnChanged += OnTurnChanged;
    }

    public override void _ExitTree()
    {
        EndTurnBtn.Pressed -= OnEndTurnBtnPressed;
        _combatSignals.TurnChanged -= OnTurnChanged;
    }

    private void UpdatePlayerUI()
    {
        PlayerNameL.Text = _playerStats.Name;
        PlayerHpPB.MaxValue = _playerStats.MaxHP;
        PlayerHpPB.Value = _playerStats.HP;
        PlayerHpL.Text = $"{_playerStats.HP}/{_playerStats.MaxHP}";
    }

    private void UpdateEnemiesUI()
    {
        TurnLabel.Text = _combatData.PlayerTurn ? "Tura gracza" : "Tura przeciwnika";
        EnemyNameL.Text = _combatData.Enemies[0].Name;
        EnemyHpPB.MaxValue = _combatData.Enemies[0].MaxHP;
        EnemyHpPB.Value = _combatData.Enemies[0].HP;
        EnemyHpL.Text = $"{_combatData.Enemies[0].MaxHP}/{_combatData.Enemies[0].HP}";
    }

    private void OnEndTurnBtnPressed()
    {
        _combatSignals.EmitTurnEnded();
    }

    private void OnTurnChanged(CombatData combatData)
    {
        _combatData = combatData;
        UpdateEnemiesUI();
    }
}
