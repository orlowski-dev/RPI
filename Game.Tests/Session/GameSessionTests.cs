public class GameSessionTests
{
    [Fact]
    public void CreateSession_ShouldInitializePlayer()
    {
        DebugExtension.Log(this, "Starting..");
        var session = Globals.CreateGameSession();
        Assert.NotNull(session.Player);
    }

    [Fact]
    public void CreateSession_ShouldStartInMainMenu()
    {
        DebugExtension.Log(this, "Starting..");
        var session = Globals.CreateGameSession();
        Assert.Equal(GameSessionState.MainMenu, session.State);
    }

    [Fact]
    public void CreateSession_ShouldContainNoDungeonAndCombatSession()
    {
        DebugExtension.Log(this, "Starting..");
        var session = Globals.CreateGameSession();
        Assert.Null(session.CombatSession);
        Assert.Null(session.Dungeon);
    }

    [Fact]
    public void CreateTwoSessions_ShouldCreateDifferentPlayers()
    {
        DebugExtension.Log(this, "Starting..");
        var s1 = Globals.CreateGameSession();
        var s2 = Globals.CreateGameSession();
        Assert.NotEqual(s1.Player.Id, s2.Player.Id);
    }

    [Fact]
    public void PlayerFactory_ShouldAssignName()
    {
        DebugExtension.Log(this, "Starting..");
        var playerF = new PlayerFactory();
        var player = playerF.Create(new(Name: "Player", Type: PlayerType.Archer));
        Assert.Equal("Player", player.Name);
    }

    [Fact]
    public void PlayerFactory_ShouldGenerateUniqueIds()
    {
        DebugExtension.Log(this, "Starting..");
        var playerF = new PlayerFactory();
        var p1 = playerF.Create(new(Name: "Player", Type: PlayerType.Archer));
        var p2 = playerF.Create(new(Name: "Player", Type: PlayerType.Archer));
        Assert.NotEqual(p1.Id, p2.Id);
    }

    [Fact]
    public void GameSession_ShouldGenerateUniqueIds()
    {
        DebugExtension.Log(this, "Starting..");
        var s1 = Globals.CreateGameSession();
        var s2 = Globals.CreateGameSession();
        Assert.NotEqual(s1.Id, s2.Id);
    }

    [Fact]
    public void CreateSession_ShouldPreservePlayerReference()
    {
        // żeby sesson nie tworzył kopii
        DebugExtension.Log(this, "Starting..");

        var player = new PlayerFactory().Create(new(Name: "player", Type: PlayerType.Warrior));
        var session = new GameSession(player);
        Assert.Same(player, session.Player);
    }
}
