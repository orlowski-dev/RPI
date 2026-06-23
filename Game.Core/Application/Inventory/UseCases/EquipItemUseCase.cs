public class EquipItemUseCase : IUseCase<EquipItemRequest, EquipItemResponse>
{
    public Result<EquipItemResponse> Execute(EquipItemRequest req)
    {
        var equip = req.Session.Inventory.Equipment.Equip(
            item: req.Item,
            playerType: req.Session.Player.Type
        );

        if (equip.IsFailure)
        {
            return Result<EquipItemResponse>.Fail(
                new(equip.Error?.Message ?? "Unknown error.", ErrorType.Validation)
            );
        }

        var response = new EquipItemResponse(Item: equip.Value);

        return Result<EquipItemResponse>.Success(response);
    }
}
