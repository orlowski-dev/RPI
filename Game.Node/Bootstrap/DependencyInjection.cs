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
        services.AddSingleton<CombatStateMachine>();
        services.AddSingleton<ArenaPresenter>();
        services.AddSingleton<ItemFactory>();
        services.AddSingleton<EventBus>();

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
        services.AddTransient<StartCombatUseCase>();
        services.AddTransient<ICombatState, PlayerTurnState>();
        services.AddTransient<ICombatState, EnemyTurnState>();
        services.AddTransient<ICombatState, ResolveTurnState>();
        services.AddTransient<ICombatState, RewardState>();
        services.AddTransient<ICombatState, PlayerDeathState>();
        services.AddTransient<ResolveTurnUseCase>();
        services.AddTransient<FinishCombatUseCase>();
        services.AddTransient<ClaimCombatRewardUseCase>();
        services.AddTransient<AddItemToInventoryUseCase>();
        services.AddTransient<GenerateShopItemsUseCase>();
        services.AddTransient<ItemShopPresenter>();
        services.AddTransient<BuyItemUseCase>();
        services.AddTransient<SellItemUseCase>();
    }
}
