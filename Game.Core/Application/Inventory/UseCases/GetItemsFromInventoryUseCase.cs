public class GetItemsFromInventoryUseCase
    : IUseCase<GetItemsFromInventoryRequest, GetItemsFromInventoryResponse>
{
    public Result<GetItemsFromInventoryResponse> Execute(GetItemsFromInventoryRequest req)
    {
        var items = req.Session.Inventory.Backpack.Items;
        return Result<GetItemsFromInventoryResponse>.Success(new(Items: items));
    }
}
