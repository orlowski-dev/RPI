using Godot;

public partial class CharacterCreatorSignals : BaseSingleton<CharacterCreatorSignals>
{
    [Signal]
    public delegate void DataSenderEventHandler(CharacterCreatorData data);

    [Signal]
    public delegate void SetSelectedClassEventHandler(string className);

    public void EmitDataSender(CharacterCreatorData data)
    {
        EmitSignal(SignalName.DataSender, data);
    }

    public void EmitSetSelectedClassName(string className)
    {
        EmitSignal(SignalName.SetSelectedClass, className);
    }
}
