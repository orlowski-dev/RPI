using Godot;

public partial class CombatSignals : BaseSingleton<CombatSignals>
{
    [Signal]
    public delegate void AttackActionEventHandler();

    [Signal]
    public delegate void DefenseActionEventHandler();

    [Signal]
    public delegate void SkipTurnEventHandler();

    [Signal]
    public delegate void DataSenderEventHandler(CombatData combatData);

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

    public void EmitDataSender(CombatData combatData)
    {
        EmitSignal(SignalName.DataSender, combatData);
    }
}
