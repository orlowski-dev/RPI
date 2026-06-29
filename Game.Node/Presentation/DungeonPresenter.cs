using Microsoft.Extensions.DependencyInjection;

public class DungeonPresenter
{
    private IGameSessionProvider _gameSessionProvider;

    public DungeonPresenter()
    {
        _gameSessionProvider =
            ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
    }

    public DungOnViewReadyViewModel OnViewReady()
    {
        if (_gameSessionProvider.Current is null)
        {
            DebugExtension.Fatal(this, "Session does not exist in the cotnext");
        }
        var res = new EnterDungeonUseCase().Execute(
            new EnterDungeonRequest(GameSession: _gameSessionProvider.Current)
        );

        return new(res.Value.Dungeon);
    }
}
