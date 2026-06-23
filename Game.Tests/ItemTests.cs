public class ItemTests
{
    [Fact]
    [LogTest]
    public void ItemFactory_ShouldGenerateItem()
    {
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
        // DebugExtension.Log(this, $"Generated item: {DebugExtension.Dump(item)}");
    }

    [Fact]
    [LogTest]
    public void ItemFactory_ShouldNotGenerateItem()
    {
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
    [LogTest]
    public void ItemBackpack_ShouldAddNewItem()
    {
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
    [LogTest]
    public void ItemBackpack_ShouldRemoveItem()
    {
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
    [LogTest]
    public void ItemBackpack_ShouldNotAddIfFull()
    {
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
    [LogTest]
    public void ItemBackpack_ShouldNotRemoveAnyItem()
    {
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
    [LogTest]
    public void ItemEquipment_ShouldEquipWeapon()
    {
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
    [LogTest]
    public void ItemEquipment_ShouldNotEquipWeapon()
    {
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
    [LogTest]
    public void ItemEquipment_ShouldRemoveItem()
    {
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
    [LogTest]
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

    [Fact]
    [LogTest]
    public void RemoveItemFromInventoryUseCase_ShouldRemoveItem()
    {
        var gameSession = Globals.CreateGameSession();
        var item = new ItemFactory().Generate("iron_sword", playerLevel: gameSession.Player.Level);
        var ucResponse = new AddItemToInventoryUseCase().Execute(
            new(Session: gameSession, Item: item)
        );
        Assert.NotEmpty(gameSession.Inventory.Backpack.Items);
        var removeResponse = new RemoveItemFromInventoryUseCase().Execute(
            new(Session: gameSession, ItemId: item.Id)
        );
        Assert.Empty(gameSession.Inventory.Backpack.Items);
    }

    [Fact]
    [LogTest]
    public void GetItemsFromInventoryUseCase_ShouldReturnItems()
    {
        var gameSession = Globals.CreateGameSession();
        var item = new ItemFactory().Generate("iron_sword", playerLevel: gameSession.Player.Level);
        var ucResponse = new AddItemToInventoryUseCase().Execute(
            new(Session: gameSession, Item: item)
        );
        var giResponse = new GetItemsFromInventoryUseCase().Execute(new(Session: gameSession));
        Assert.NotNull(giResponse.Value);
        Assert.NotEmpty(giResponse.Value.Items);
    }

    [Fact]
    [LogTest]
    public void EquipItemUseCase_ShouldIncreasePlayerStats()
    {
        // tworzę playera w ui
        var name = "Player1";
        var type = PlayerType.Warrior;
        // tworzę sesje po kliknięciu w rozpocznij grę w kreatorze potaci
        var newGameResponse = new StartNewGameUseCase()
            .Execute(new StartNewGameRequest(PlayerName: name, PlayerType: type))
            .Value;
        var player = newGameResponse.GameSession.Player;
        var startAttack = player.Stats.Attack;

        // generuje jakiś itemek
        var item = new ItemFactory().Generate("iron_sword", playerLevel: player.Level); // ten itemek jest dla warrior tylko

        // dodaje go do plecaka
        var addItemResponse = new AddItemToInventoryUseCase().Execute(
            new AddItemToInventoryRequest(Session: newGameResponse.GameSession, Item: item)
        );

        // zakładam przedmiot
        var equipResult = new EquipItemUseCase().Execute(
            new EquipItemRequest(Session: newGameResponse.GameSession, Item: item)
        );

        // skrot do game session
        var session = newGameResponse.GameSession;

        // sprawdzam czy item jest zalozony w eq
        Assert.NotNull(session.Inventory.Equipment.Weapon);

        // sprawdzam czy staty się podiosły - tu atak
        Assert.True(startAttack < session.Player.Stats.Attack);
    }
}
