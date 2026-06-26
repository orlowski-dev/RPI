using Godot;

public partial class CityViewScript : Control
{
    private enum Pb
    {
        Hp,
        Exp,
    }

    private enum Lbl {
        PlayerName
    }

    private Dictionary<Pb, TextureProgressBar> _bars = null!;
    private Dictionary<Lbl, Label> _labels = null!:

    public override void _Ready() { }
}
