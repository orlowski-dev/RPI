public class AddItemToInventoryUseCase
    : IUseCase<AddItemToInventoryRequest, AddItemToInventoryResponse>
{
    public Result<AddItemToInventoryResponse> Execute(AddItemToInventoryRequest req)
    {
        if (req.GameSession is null)
        {
            DebugExtension.Fatal(this, "GameSession in request is null.");
        }

        if (req.Item is null)
        {
            DebugExtension.Fatal(this, "Item in request is null.");
        }

        var add = req.GameSession.Inventory.Backpack.Add(req.Item);

        if (add.IsFailure)
        {
            return Result<AddItemToInventoryResponse>.Fail(
                new(add.Error?.Message ?? "Unknown Error.", ErrorType.Validation)
            );
        }

        return Result<AddItemToInventoryResponse>.Success(new());
    }
}
