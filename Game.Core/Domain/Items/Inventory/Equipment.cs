public class Equipment
{
    public Item? Weapon { get; private set; }
    public Item? Armor { get; private set; }

    public Equipment(Item? weapon = null, Item? armor = null)
    {
        Weapon = weapon;
        Armor = armor;
    }

    public Result<Item> Equip(Item item, PlayerType playerType)
    {
        if (!item.AllowedClasses.Contains(playerType))
        {
            return Result<Item>.Fail(
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

        return Result<Item>.Success(item);
    }

    public Result Remove(Guid id)
    {
        if (Armor?.Id == id)
        {
            Armor = null;
            return Result.Success();
        }

        if (Weapon?.Id == id)
        {
            Weapon = null;
            return Result.Success();
        }

        return Result.Fail(
            new("Item with given id does not exist or isnt equipmend.", ErrorType.NotFound)
        );
    }
}
