public class BuyItemUseCase : IUseCase<BuyItemRequest, BuyItemResponse>
{
    private readonly IGameSessionProvider _gs;
    private Backpack? _Backpack => _gs.Current?.Inventory.Backpack;
    private Player? _Player => _gs.Current?.Player;

    public BuyItemUseCase(IGameSessionProvider gs)
    {
        _gs = gs;
    }

    public Result<BuyItemResponse> Execute(BuyItemRequest req)
    {
        if (_gs.Current is null || _Backpack is null || _Player is null)
        {
            DebugExtension.Fatal(this, "One or more of required values are null");
        }

        var price = ItemCatalog.CalcultePrice(req.Item.Stats);

        if (price > _Player.Gold)
        {
            return Result<BuyItemResponse>.Fail(
                new Error("not enought money", ErrorType.Validation)
            );
        }

        _Player.SubstractGold(price);
        _Backpack.Items.Add(req.Item);

        var response = new BuyItemResponse();
        return Result<BuyItemResponse>.Success(response);
    }
}
