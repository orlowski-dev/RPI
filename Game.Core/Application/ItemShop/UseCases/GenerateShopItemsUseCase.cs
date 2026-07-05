public class GenerateShopItemsUseCase
    : IUseCase<GenerateShopItemsRequest, GenerateShopItemsResponse>
{
    private const int ItemsToGenerate = 10;
    private readonly IGameSessionProvider _gs = null!;
    private readonly ItemFactory _itemFactory = null!;

    public GenerateShopItemsUseCase(IGameSessionProvider gs, ItemFactory itemFactory)
    {
        _gs = gs;
        _itemFactory = itemFactory;
    }

    public Result<GenerateShopItemsResponse> Execute(GenerateShopItemsRequest req)
    {
        if (_gs.Current is null)
        {
            DebugExtension.Fatal(this, "Current game session is null!");
        }

        var items = new List<Item>();
        for (var i = 0; i < ItemsToGenerate; i++)
        {
            items.Add(_itemFactory.GenerateRandom(playerLevel: _gs.Current.Player.Level));
        }

        _gs.Current.ShopItems = items;

        var response = new GenerateShopItemsResponse(Items: items);
        return Result<GenerateShopItemsResponse>.Success(response);
    }
}
