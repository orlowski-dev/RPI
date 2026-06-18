public class GameSession
{
    public readonly Guid Id;
    public Player Player { get; }
    public GameSessionState State { get; private set; }
    public CombatSession? CombatSession { get; private set; }
    public Dungeon? Dungeon { get; private set; }
    public List<string>? Inventory { get; private set; }

    public GameSession(Player player)
    {
        Id = Guid.NewGuid();
        State = GameSessionState.MainMenu;
        Player = player;
    }

    public GameSession(Guid id, Player player, GameSessionState state, Dungeon? dungeon)
    {
        Id = id;
        Player = player;
        State = state;
        Dungeon = dungeon;
    }

    public void EnterDungeon(Dungeon dungeon)
    {
        DebugExtension.Log(this, $"Setting dungeon {dungeon.Id} in GameSession..");
        Dungeon = dungeon;
    }
}
