using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class ArenaViewScript : Control
{
    private enum Bt
    {
        Attack,
    }

    private Dictionary<Bt, Button> _buttons = new();

    private IGameSessionProvider _gsProvider = null!;
    private ArenaPresenter _arenaPresenter = null!;
    private CombatSession _session => _gsProvider.Current!.CombatSession!;

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
    }

    private void OnAttackButtonClick()
    {
        var res = _arenaPresenter.OnPlayerAttackAction();
        GD.Print(res);
    }
}
