public class UnequipItemUseCase : IUseCase<UnequipItemRequest, UnequipItemResponse>
{
    public Result<UnequipItemResponse> Execute(UnequipItemRequest req)
    {
        var result = req.GameSession.Inventory.Equipment.Remove(req.ItemId);

        var response = new UnequipItemResponse();
        return Result<UnequipItemResponse>.Success(response);
    }
}
