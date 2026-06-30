using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class ArenaViewScript : Control
{
    private enum Bt
    {
        Attack,
    }

    [Export]
    public float ActionDelay = 1.0f;

    private Dictionary<Bt, Button> _buttons = new();
    private Label _turnLabel = null!;

    private IGameSessionProvider _gsProvider = null!;
    private ArenaPresenter _arenaPresenter = null!;
    private ArenaScene? _arenaScene;

    public override void _Ready()
    {
        _gsProvider = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
        _arenaPresenter = ServiceProviderHolder.Provider.GetRequiredService<ArenaPresenter>();

        GetNodes();
    }

    private void GetNodes()
    {
        _buttons[Bt.Attack] = GetNode<Button>("%AttackButton");
        _buttons[Bt.Attack].Pressed += OnAttackButtonClick;
        _turnLabel = GetNode<Label>("%TurnLabel");
    }

    private async void OnAttackButtonClick()
    {
        // var res = _arenaPresenter.OnPlayerAttackAction();
        // GD.Print(_gsProvider.Current?.CombatSession?.GetInfo() ?? "Combat session does not exist!");

        // await ToSignal(GetTree().CreateTimer(ActionDelay), Godot.Timer.SignalName.Timeout);

        _arenaPresenter.OnPlayerAttackAction();
        _arenaScene?.StartCombatSteps();
    }

    public void SetArenaScene(ArenaScene arenaScene)
    {
        _arenaScene = arenaScene;
    }
}
