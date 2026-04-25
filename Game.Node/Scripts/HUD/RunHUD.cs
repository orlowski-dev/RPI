using Godot;

public partial class RunHUD : Node
{
    private LevelSignals LevelSignals => LevelSignals.Instance;

    [Export]
    ProgressBar PlayerHpPB { get; set; }

    [Export]
    Label PlayerHpL { get; set; }

    [Export]
    ProgressBar PlayerExpPB { get; set; }

    [Export]
    Label PlayerExpL { get; set; }

    [Export]
    Label PlayerNameL { get; set; }

    [Export]
    Label PlayerLevelL { get; set; }

    public override void _Ready()
    {
        LevelSignals.DataSender += OnDataSenderEvent;
    }

    public override void _ExitTree()
    {
        LevelSignals.DataSender -= OnDataSenderEvent;
    }

    private void UpdateUI(CityHudData data)
    {
        PlayerNameL.Text = data.PlayerCharacter.Name;
        PlayerHpPB.MinValue = 0;
        PlayerHpPB.MaxValue = data.PlayerCharacter.MaxHP;
        PlayerHpPB.Value = data.PlayerCharacter.HP;
        PlayerHpL.Text = $"{data.PlayerCharacter.HP}/{data.PlayerCharacter.MaxHP}";

        PlayerExpPB.MinValue = 0;
        PlayerExpPB.MaxValue = data.PlayerCharacter.ExpNextLvl;
        PlayerExpPB.Value = data.PlayerCharacter.Exp;
        PlayerExpL.Text = $"{data.PlayerCharacter.Exp}/{data.PlayerCharacter.ExpNextLvl}";
        PlayerLevelL.Text = $"( Lvl: {data.PlayerCharacter.Level} )";
    }

    private void OnDataSenderEvent(CityHudData data)
    {
        UpdateUI(data);
    }
}
