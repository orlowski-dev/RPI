public class ItemTests
{
    [Fact]
    public void ItemFactory_ShouldGenerateItem()
    {
        DebugExtension.Log(this, "Starting..");

        // ["iron_sword"] = new(
        //     Name: "Żelazny miecz",
        //     Category: ItemCategory.Weapon,
        //     BaseStats: new(0, 12, 2, 3, 0),
        //     AllowedClasses: [PlayerType.Warrior]
        // )
        var catalogId = "iron_sword";
        var itemFactory = new ItemFactory();
        var item = itemFactory.Generate(id: catalogId, playerLevel: 1);
        Assert.NotNull(item);
        DebugExtension.Log(this, $"Generated item: {DebugExtension.Dump(item)}");
    }

    [Fact]
    public void ItemFactory_ShouldNotGenerateItem()
    {
        DebugExtension.Log(this, "Starting..");

        // ["iron_sword"] = new(
        //     Name: "Żelazny miecz",
        //     Category: ItemCategory.Weapon,
        //     BaseStats: new(0, 12, 2, 3, 0),
        //     AllowedClasses: [PlayerType.Warrior]
        // )
        var catalogId = "iron_sworda";
        var itemFactory = new ItemFactory();
        var item = Assert.Throws<Exception>(() =>
            itemFactory.Generate(id: catalogId, playerLevel: 1)
        );
    }

    [Fact]
    public void ItemBackpack_ShouldAddNewItem()
    {
        DebugExtension.Log(this, "Starting..");

        // ["iron_sword"] = new(
        //     Name: "Żelazny miecz",
        //     Category: ItemCategory.Weapon,
        //     BaseStats: new(0, 12, 2, 3, 0),
        //     AllowedClasses: [PlayerType.Warrior]
        // )
        var catalogId = "iron_sword";
        var itemFactory = new ItemFactory();
        var item = itemFactory.Generate(id: catalogId, playerLevel: 1);
        var backpack = new Backpack();
        backpack.Add(item);
        Assert.NotEmpty(backpack.Items);
    }

    [Fact]
    public void ItemBackpack_ShouldRemoveItem()
    {
        DebugExtension.Log(this, "Starting..");

        // ["iron_sword"] = new(
        //     Name: "Żelazny miecz",
        //     Category: ItemCategory.Weapon,
        //     BaseStats: new(0, 12, 2, 3, 0),
        //     AllowedClasses: [PlayerType.Warrior]
        // )
        var catalogId = "iron_sword";
        var itemFactory = new ItemFactory();
        var item = itemFactory.Generate(id: catalogId, playerLevel: 1);
        var backpack = new Backpack();
        backpack.Add(item);
        Assert.NotEmpty(backpack.Items);
        backpack.Remove(item.Id);
        Assert.Empty(backpack.Items);
    }

    [Fact]
    public void ItemBackpack_ShouldNotAddIfFull()
    {
        DebugExtension.Log(this, "Starting..");

        // ["iron_sword"] = new(
        //     Name: "Żelazny miecz",
        //     Category: ItemCategory.Weapon,
        //     BaseStats: new(0, 12, 2, 3, 0),
        //     AllowedClasses: [PlayerType.Warrior]
        // )
        var catalogId = "iron_sword";
        var itemFactory = new ItemFactory();
        var item = itemFactory.Generate(id: catalogId, playerLevel: 1);
        var backpack = new Backpack();
        backpack.SetCapacity(0);
        var add = backpack.Add(item);
        Assert.NotNull(add.Error);
        Assert.Equal(ErrorType.Validation, add.Error.Type);
    }

    [Fact]
    public void ItemBackpack_ShouldNotRemoveAnyItem()
    {
        DebugExtension.Log(this, "Starting..");

        // ["iron_sword"] = new(
        //     Name: "Żelazny miecz",
        //     Category: ItemCategory.Weapon,
        //     BaseStats: new(0, 12, 2, 3, 0),
        //     AllowedClasses: [PlayerType.Warrior]
        // )
        var catalogId = "iron_sword";
        var itemFactory = new ItemFactory();
        var item = itemFactory.Generate(id: catalogId, playerLevel: 1);
        var item2 = itemFactory.Generate(id: catalogId, playerLevel: 1);
        var backpack = new Backpack();
        backpack.Add(item);
        backpack.Add(item2);
        var remove = backpack.Remove(Guid.NewGuid());
        Assert.NotNull(remove.Error);
        Assert.Equal(ErrorType.NotFound, remove.Error.Type);
        Assert.Contains(item, backpack.Items);
        Assert.Contains(item2, backpack.Items);
    }

    [Fact]
    public void ItemEquipment_ShouldEquipWeapon()
    {
        DebugExtension.Log(this, "Starting..");

        // ["iron_sword"] = new(
        //     Name: "Żelazny miecz",
        //     Category: ItemCategory.Weapon,
        //     BaseStats: new(0, 12, 2, 3, 0),
        //     AllowedClasses: [PlayerType.Warrior]
        // )
        var catalogId = "iron_sword";
        var itemFactory = new ItemFactory();
        var item = itemFactory.Generate(id: catalogId, playerLevel: 1);
        var eq = new Equipment();
        var backpack = new Backpack();
        backpack.Add(item);
        eq.Equip(item, PlayerType.Warrior);
        Assert.NotNull(eq.Weapon);
        Assert.Null(eq.Armor);
    }

    [Fact]
    public void ItemEquipment_ShouldNotEquipWeapon()
    {
        DebugExtension.Log(this, "Starting..");

        // ["iron_sword"] = new(
        //     Name: "Żelazny miecz",
        //     Category: ItemCategory.Weapon,
        //     BaseStats: new(0, 12, 2, 3, 0),
        //     AllowedClasses: [PlayerType.Warrior]
        // )
        var catalogId = "iron_sword";
        var itemFactory = new ItemFactory();
        var item = itemFactory.Generate(id: catalogId, playerLevel: 1);
        var eq = new Equipment();
        var backpack = new Backpack();
        backpack.Add(item);
        var equip = eq.Equip(item, PlayerType.Archer);
        Assert.NotNull(equip.Error);
        Assert.Equal(ErrorType.Validation, equip.Error.Type);
        Assert.Null(eq.Weapon);
        Assert.Null(eq.Armor);
    }

    [Fact]
    public void ItemEquipment_ShouldRemoveItem()
    {
        DebugExtension.Log(this, "Starting..");

        // ["iron_sword"] = new(
        //     Name: "Żelazny miecz",
        //     Category: ItemCategory.Weapon,
        //     BaseStats: new(0, 12, 2, 3, 0),
        //     AllowedClasses: [PlayerType.Warrior]
        // )
        // ["plate_armor"] = new(
        //     Name: "Pancerz płytkowy",
        //     Category: ItemCategory.Armor,
        //     BaseStats: new(15, 0, 16, 0, 0),
        //     AllowedClasses: [PlayerType.Archer, PlayerType.Mage, PlayerType.Warrior]
        // ),
        var itemFactory = new ItemFactory();
        var weapon = itemFactory.Generate(id: "iron_sword", playerLevel: 1);
        var armor = itemFactory.Generate(id: "plate_armor", playerLevel: 1);
        var eq = new Equipment();
        var backpack = new Backpack();
        backpack.Add(weapon);
        backpack.Add(armor);
        var equip = eq.Equip(weapon, PlayerType.Archer);
        eq.Equip(armor, PlayerType.Archer);
        eq.Remove(weapon.Id);
        Assert.Null(eq.Weapon);
        Assert.Equal(armor, eq.Armor);
    }

    [Fact]
    public void AddItemToBackpackUseCase_ShouldAddItem()
    {
        var gameSession = Globals.CreateGameSession();
        var item = new ItemFactory().Generate("iron_sword", playerLevel: gameSession.Player.Level);
        Assert.Empty(gameSession.Inventory.Backpack.Items);
        var ucResponse = new AddItemToInventoryUseCase().Execute(
            new(Session: gameSession, Item: item)
        );
        Assert.NotEmpty(gameSession.Inventory.Backpack.Items);
    }
}
