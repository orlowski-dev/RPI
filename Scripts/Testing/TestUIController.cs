using System;
using Godot;

public partial class TestUIController : Node
{
    [Export]
    Button IncreaseHealthBtn;

    [Export]
    Button DecreaseHealthBtn;

    [Export]
    ProgressBar HpProgressB;

    private GameController _gc => GameController.Instance;
    private PlayerCharacter _pc => _gc.PlayerCharacter;
    private Signals _signals => Signals.Instance;

    public override void _Ready()
    {
        _gc.StartNewGame();
        Logger.Write(LogLevel.Info, this.GetType().Name, "To jest zwykła informacja");
        Logger.Write(LogLevel.Warning, this.GetType().Name, "To jest ostatnie ostrzeżenie!");
        Logger.Write(LogLevel.Error, this.GetType().Name, "To jest błąd.");

        HpProgressB.MaxValue = _pc.MaxHP;
        HpProgressB.Value = _pc.HP;

        _pc.HpChanged += OnHealthChanged;

        IncreaseHealthBtn.Pressed += IncreaseHealthPressed;
        DecreaseHealthBtn.Pressed += DecreaseHealthPressed;
    }

    public override void _ExitTree()
    {
        // Odsubskrybuj sygnał przy usunięciu węzła - AI :/
        _pc.HpChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int newHealth)
    {
        HpProgressB.Value = newHealth;
    }

    private void IncreaseHealthPressed()
    {
        _pc.Heal(10);
    }

    private void DecreaseHealthPressed()
    {
        _pc.TakeDamage(10);
    }
}
