public class SellItemUseCase : IUseCase<SellItemRequest, SellItemResponse>
{
    private readonly IGameSessionProvider _gs;
    private Backpack? _Backpack => _gs.Current?.Inventory.Backpack;
    private Player? _Player => _gs.Current?.Player;

    public SellItemUseCase(IGameSessionProvider gs)
    {
        _gs = gs;
    }

    public Result<SellItemResponse> Execute(SellItemRequest req)
    {
        if (_gs.Current is null || _Backpack is null || _Player is null)
        {
            DebugExtension.Fatal(this, "One or more of required values are null");
        }

        var sellPrice = ItemCatalog.CalculateSellPrice(req.Item.Stats);

        _Player.AddGold(sellPrice);
        _Backpack.Items.Remove(req.Item);

        var response = new SellItemResponse();
        return Result<SellItemResponse>.Success(response);
    }
}
