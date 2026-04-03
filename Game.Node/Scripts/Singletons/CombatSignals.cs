using System;
using Godot;

public partial class CombatSignals : BaseSingleton<CombatSignals>
{
    [Signal]
    public delegate void TurnEndedEventHandler();

    [Signal]
    public delegate void TurnChangedEventHandler(bool playerTurn);

    public void EmitTurnEnded()
    {
        EmitSignal(SignalName.TurnEnded);
        GD.Print("TurnEnded signal emited");
    }

    public void EmitTurnChanged(bool playerTurn)
    {
        EmitSignal(SignalName.TurnChanged, playerTurn);
    }
}
