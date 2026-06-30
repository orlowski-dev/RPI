using Microsoft.Extensions.DependencyInjection;

// lista usług
public static class DependencyInjection
{
    public static IServiceProvider Build()
    {
        var services = new ServiceCollection();
        RegisterCore(services);
        RegisterApplication(services);
        RegisterNode(services);

        return services.BuildServiceProvider();
    }

    private static void RegisterCore(IServiceCollection services) { }

    private static void RegisterApplication(IServiceCollection services) { }

    private static void RegisterNode(IServiceCollection services)
    {
        services.AddSingleton<IGameSessionProvider, GameSessionProvider>();
        services.AddSingleton<GameSessionFactory>();
        services.AddSingleton<Random>();

        services.AddTransient<MainMenuPresenter>();
        services.AddTransient<CharacterCreatorPresenter>();
        services.AddTransient<StartNewGameUseCase>();
        services.AddTransient<GetStartCharactersUseCase>();
        services.AddTransient<CityPresenter>();
        services.AddTransient<PlayerControllerPresenter>();
        services.AddTransient<PortalPresenter>();
        services.AddTransient<DungeonPresenter>();
        services.AddTransient<EnemyPresenter>();
        services.AddTransient<EnemyFactory>();
        services.AddTransient<EncounterFactory>();
    }
}
