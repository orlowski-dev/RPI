// główny
public record GameSnapshot(
    DateTime CreatedAt,
    Guid Id,
    GameSessionState State,
    PlayerSnapshot Player,
    InventorySnapshot Inventory,
    DungeonSnapshot? Dungeon
);
