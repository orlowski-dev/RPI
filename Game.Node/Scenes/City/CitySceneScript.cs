using Godot;
using Microsoft.Extensions.DependencyInjection;

public partial class CitySceneScript : Node
{
    private CityPresenter _presenter = null!;
    private IGameSessionProvider _gameSessionProvider = null!;
    private Player _player =>
        _gameSessionProvider.Current?.Player ?? throw new Exception("Player instance is null.");

    public override void _Ready()
    {
        _presenter = ServiceProviderHolder.Provider.GetRequiredService<CityPresenter>();
        _gameSessionProvider =
            ServiceProviderHolder.Provider.GetRequiredService<IGameSessionProvider>();
    }
}
