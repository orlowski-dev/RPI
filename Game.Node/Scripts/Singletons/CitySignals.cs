using Godot;

public partial class CitySignals : BaseSingleton<CitySignals>
{
    [Signal]
    public delegate void DataSenderEventHandler(CityHudData cityHudData);

    public void EmitDataSender(CityHudData cityHudData)
    {
        EmitSignal(SignalName.DataSender, cityHudData);
    }
}
