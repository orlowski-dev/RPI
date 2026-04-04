using Godot;

public partial class CombatSignals : BaseSingleton<CombatSignals>
{
    [Signal]
    public delegate void TurnChangedEventHandler(CombatData combatInitData);

    [Signal]
    public delegate void AttackActionEventHandler();

    [Signal]
    public delegate void DefenseActionEventHandler();

    [Signal]
    public delegate void SkipTurnEventHandler();

    public void EmitTurnChanged(CombatData combatData)
    {
        EmitSignal(SignalName.TurnChanged, combatData);
    }

    public void EmitAttackAction()
    {
        EmitSignal(SignalName.AttackAction);
    }

    public void EmitDefenseAction()
    {
        EmitSignal(SignalName.DefenseAction);
    }

    public void EmitSkipTurn()
    {
        EmitSignal(SignalName.SkipTurn);
    }
}
