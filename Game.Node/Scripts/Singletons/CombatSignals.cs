using Godot;

public partial class CombatSignals : BaseSingleton<CombatSignals>
{
    [Signal]
    public delegate void TurnEndedEventHandler();

    [Signal]
    public delegate void TurnChangedEventHandler(CombatData combatInitData);

    public void EmitTurnEnded()
    {
        EmitSignal(SignalName.TurnEnded);
    }

    public void EmitTurnChanged(CombatData combatData)
    {
        EmitSignal(SignalName.TurnChanged, combatData);
    }
}
