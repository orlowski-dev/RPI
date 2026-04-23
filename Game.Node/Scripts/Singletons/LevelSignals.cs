using Godot;

public partial class LevelSignals : BaseSingleton<LevelSignals>
{
    [Signal]
    public delegate void DataSenderEventHandler(CityHudData cityHudData);

    public void EmitDataSender(CityHudData cityHudData)
    {
        EmitSignal(SignalName.DataSender, cityHudData);
    }
}
