public class GameSessionTests
{
    [Fact]
    [LogTest]
    public void CreateSession_ShouldInitializePlayer()
    {
        var session = Globals.CreateGameSession();
        Assert.NotNull(session.Player);
    }

    [Fact]
    [LogTest]
    public void CreateSession_ShouldStartInMainMenu()
    {
        var session = Globals.CreateGameSession();
        Assert.Equal(GameSessionState.MainMenu, session.State);
    }

    [Fact]
    [LogTest]
    public void CreateSession_ShouldContainNoDungeonAndCombatSession()
    {
        var session = Globals.CreateGameSession();
        Assert.Null(session.CombatSession);
        Assert.Null(session.Dungeon);
    }

    [Fact]
    [LogTest]
    public void CreateTwoSessions_ShouldCreateDifferentPlayers()
    {
        var s1 = Globals.CreateGameSession();
        var s2 = Globals.CreateGameSession();
        Assert.NotEqual(s1.Player.Id, s2.Player.Id);
    }

    [Fact]
    [LogTest]
    public void PlayerFactory_ShouldAssignName()
    {
        var playerF = new PlayerFactory();
        var player = playerF.Create(new(Name: "Player", Type: PlayerType.Archer));
        Assert.Equal("Player", player.Name);
    }

    [Fact]
    [LogTest]
    public void PlayerFactory_ShouldGenerateUniqueIds()
    {
        var playerF = new PlayerFactory();
        var p1 = playerF.Create(new(Name: "Player", Type: PlayerType.Archer));
        var p2 = playerF.Create(new(Name: "Player", Type: PlayerType.Archer));
        Assert.NotEqual(p1.Id, p2.Id);
    }

    [Fact]
    [LogTest]
    public void GameSession_ShouldGenerateUniqueIds()
    {
        var s1 = Globals.CreateGameSession();
        var s2 = Globals.CreateGameSession();
        Assert.NotEqual(s1.Id, s2.Id);
    }

    [Fact]
    [LogTest]
    public void CreateSession_ShouldPreservePlayerReference()
    {
        // żeby sesson nie tworzył kopii

        var player = new PlayerFactory().Create(new(Name: "player", Type: PlayerType.Warrior));
        var session = new GameSession(player);
        Assert.Same(player, session.Player);
    }
}
