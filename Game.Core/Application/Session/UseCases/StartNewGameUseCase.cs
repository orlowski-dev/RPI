public class StartNewGameUseCase : IUseCase<StartNewGameRequest, StartNewGameResponse>
{
    private readonly IGameSessionProvider _session;
    private readonly GameSessionFactory _factory;
    private readonly ItemFactory _itemFactory;

    public StartNewGameUseCase(
        IGameSessionProvider session,
        GameSessionFactory factory,
        ItemFactory itemFactory
    )
    {
        _session = session;
        _factory = factory;
        _itemFactory = itemFactory;
    }

    public Result<StartNewGameResponse> Execute(StartNewGameRequest req)
    {
        var gSession = _factory.Create(playerName: req.PlayerName, playerType: req.PlayerType);
        _session.Set(gSession);

        Item startItem = default!;

        do
        {
            startItem = _itemFactory.GenerateRandom(gSession.Player.Level);
        } while (!startItem.AllowedClasses.Any((cl) => cl == gSession.Player.Type));

        gSession.Inventory.Backpack.Add(startItem);

        return Result<StartNewGameResponse>.Success(
            new StartNewGameResponse(GameSession: gSession)
        );
    }
}
