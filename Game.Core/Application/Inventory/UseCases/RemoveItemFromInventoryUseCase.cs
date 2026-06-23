public class RemoveItemFromInventoryUseCase
    : IUseCase<RemoveItemFromInventoryRequest, RemoveItemFromInventoryResponse>
{
    public Result<RemoveItemFromInventoryResponse> Execute(RemoveItemFromInventoryRequest req)
    {
        var removed = req.Session.Inventory.Backpack.Remove(req.ItemId);

        if (removed.IsFailure)
        {
            return Result<RemoveItemFromInventoryResponse>.Fail(
                new(removed.Error?.Message ?? "Unknown error.", ErrorType.Validation)
            );
        }

        return Result<RemoveItemFromInventoryResponse>.Success(new());
    }
}
