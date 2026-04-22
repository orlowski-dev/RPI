using Godot;

public partial class CityHUD : Node
{
    [Export]
    ProgressBar PlayerHpPB { get; set; }

    [Export]
    Label PlayerHpL { get; set; }

    [Export]
    ProgressBar PlayerExpPB { get; set; }

    [Export]
    Label PlayerExpL { get; set; }

    public override void _Ready() { }

    private void UpdateUI() { }
}
