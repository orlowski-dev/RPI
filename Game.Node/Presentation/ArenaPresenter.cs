using Microsoft.Extensions.DependencyInjection;

public class ArenaPresenter
{
    private StartCombatUseCase _scUseCase = null!;
    private IGameSessionProvider _gsProvider = null!;
    private CombatSession _session => _gsProvider.Current!.CombatSession!;

    public ArenaOnViewReadyVM OnViewReady(Player player)
    {
        _scUseCase = ServiceProviderHolder.Provider.GetRequiredService<StartCombatUseCase>();
        _gsProvider = ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
        var session = ServiceProviderHolder
            .Provider.GetRequiredService<IGameSessionProvider>()
            .Current!;
        var encounter =
            session.PendingEncounter
            ?? throw new InvalidOperationException("No pending encounter set!");

        var res = _scUseCase.Execute(
            new StartCombatRequest(Player: player, Enemies: encounter.Enemies)
        );
        if (res.IsFailure)
        {
            DebugExtension.Fatal(this, res.Error!.Message);
        }
        return new(CombatSession: res.Value.CombatSession);
    }

    public ArenaOnAttackVM OnPlayerAttackAction()
    {
        return new();
    }
}
