using Godot;

public partial class CombatSignals : BaseSingleton<CombatSignals>
{
    [Signal]
    public delegate void TurnEndedEventHandler();

    [Signal]
    public delegate void TurnChangedEventHandler(CombatData combatInitData);

    [Signal]
    public delegate void AttackActionEventHandler();

    public void EmitTurnEnded()
    {
        EmitSignal(SignalName.TurnEnded);
    }

    public void EmitTurnChanged(CombatData combatData)
    {
        EmitSignal(SignalName.TurnChanged, combatData);
    }

    public void EmitAttackAction()
    {
        EmitSignal(SignalName.AttackAction);
    }
}
