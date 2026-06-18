public class Equipment
{
    private PlayerType _playerType;
    public Item? Weapon { get; private set; }
    public Item? Armor { get; private set; }

    public Equipment(PlayerType playerType)
    {
        _playerType = playerType;
    }

    public Result Equip(Item item)
    {
        if (!item.AllowedClasses.Contains(_playerType))
        {
            return Result.Fail(
                new($"Cannot equip this item for this character class!", ErrorType.Validation)
            );
        }

        if (item.Category == ItemCategory.Weapon)
        {
            Weapon = item;
        }
        else if (item.Category == ItemCategory.Armor)
        {
            Armor = item;
        }

        return Result.Success();
    }
}
