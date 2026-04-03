using System;
using Godot;

public partial class CombatController : Node
{
    private CombatService _service;
    private Signals _signals => Signals.Instance;
    private CombatSignals _combatSignals => CombatSignals.Instance;

    public override void _Ready()
    {
        _combatSignals.TurnEnded += OnTurnEnded;
        _service = new CombatService();
        _combatSignals.EmitTurnChanged(_service.PlayerTurn);
    }

    public override void _ExitTree()
    {
        _combatSignals.TurnEnded -= OnTurnEnded;
    }

    private void OnTurnEnded()
    {
        GD.Print("Turn ended singal received");
        _service.ChangeTurn();
        _combatSignals.EmitTurnChanged(_service.PlayerTurn);
        GD.Print("TurnChanged signal emited");
    }
}
