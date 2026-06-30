using Microsoft.Extensions.DependencyInjection;

public class ArenaPresenter
{
    private StartCombatUseCase _scUseCase = null!;

    public ArenaOnViewReadyVM OnViewReady(Player player)
    {
        _scUseCase = ServiceProviderHolder.Provider.GetRequiredService<StartCombatUseCase>();
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
}
