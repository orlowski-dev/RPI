using Godot;

public partial class TestUIController : Node
{
    [Export]
    Button IncreaseHealthBtn;

    [Export]
    Button DecreaseHealthBtn;

    [Export]
    ProgressBar HpProgressB;

    // private GameController _gc => GameController.Instance;
    // private PlayerCharacter _pc => _gc.PlayerCharacter;
    private Signals _signals => Signals.Instance;
    private Logger _logger => Logger.Instance;

    public override void _Ready()
    {
        // _gc.StartNewGame();
        _logger.Write(LogLevel.Info, this.GetType().Name, "Test message");

        // HpProgressB.MaxValue = _pc.MaxHP;
        // HpProgressB.Value = _pc.HP;

        _signals.CharacterHpChanged += OnHealthChanged;

        IncreaseHealthBtn.Pressed += IncreaseHealthPressed;
        DecreaseHealthBtn.Pressed += DecreaseHealthPressed;
    }

    public override void _ExitTree()
    {
        // Odsubskrybuj sygnał przy usunięciu węzła - AI :/
        _signals.CharacterHpChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int newHealth)
    {
        HpProgressB.Value = newHealth;
    }

    private void IncreaseHealthPressed()
    {
        // _pc.Heal(10);
    }

    private void DecreaseHealthPressed()
    {
        // _pc.TakeDamage(10);
    }
}
