public class GameSession
{
    public readonly Guid Id;
    public Player Player { get; }
    public GameSessionState State { get; private set; }
    public CombatSession? CombatSession { get; private set; }
    public Dungeon? Dungeon { get; private set; }
    public Inventory Inventory { get; }

    public GameSession(Player player)
    {
        Id = Guid.NewGuid();
        State = GameSessionState.MainMenu;
        Player = player;
        Inventory = new Inventory();
    }

    public GameSession(
        Guid id,
        Player player,
        GameSessionState state,
        Inventory inventory,
        Dungeon? dungeon = null
    )
    {
        Id = id;
        Player = player;
        State = state;
        Dungeon = dungeon;
        Inventory = inventory;
    }

    public void EnterDungeon(Dungeon dungeon)
    {
        // DebugExtension.Log(this, $"Setting dungeon {dungeon.Id} in GameSession..");
        Dungeon = dungeon;
    }
}
