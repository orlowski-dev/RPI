using System;
using Godot;

public partial class CombatHUD : Node
{
    [Export]
    Button EndTurnBtn { get; set; }

    [Export]
    Label TurnLabel { get; set; }

    private CombatSignals _combatSignals => CombatSignals.Instance;

    public override void _Ready()
    {
        EndTurnBtn.Pressed += OnEndTurnBtnPressed;
        _combatSignals.TurnChanged += OnTurnChanged;
    }

    public override void _ExitTree()
    {
        EndTurnBtn.Pressed -= OnEndTurnBtnPressed;
        _combatSignals.TurnChanged -= OnTurnChanged;
    }

    private void OnEndTurnBtnPressed()
    {
        GD.Print("Butonn clicked");
        _combatSignals.EmitTurnEnded();
    }

    private void OnTurnChanged(bool playerTurn)
    {
        TurnLabel.Text = playerTurn ? "Tura gracza" : "Tura przeciwnika";
        EndTurnBtn.Disabled = playerTurn ? false : true;
    }
}
