using Game.Tests.Combat;

public class GameSessionTests
{
    private GameSession CreateGameSession()
    {
        var tcp = new TestCombatParticipant();
        var playerFactory = new PlayerFactory();

        var factory = new GameSessionFactory();

        var session = factory.Create(
            new(
                Player: new(
                    Name: Guid.NewGuid().ToString(),
                    Stats: tcp.Player.Stats,
                    Type: tcp.Player.Type
                )
            )
        );

        return session;
    }

    [Fact]
    public void CreateSession_ShouldInitializePlayer()
    {
        DebugExtension.Log(this, "Starting..");
        var session = CreateGameSession();
        Assert.NotNull(session.Player);
    }

    [Fact]
    public void CreateSession_ShouldStartInMainMenu()
    {
        DebugExtension.Log(this, "Starting..");
        var session = CreateGameSession();
        Assert.Equal(GameSessionState.MainMenu, session.State);
    }

    [Fact]
    public void CreateSession_ShouldContainNoDungeonAndCombatSession()
    {
        DebugExtension.Log(this, "Starting..");
        var session = CreateGameSession();
        Assert.Null(session.CombatSession);
        Assert.Null(session.Dungeon);
    }

    [Fact]
    public void CreateTwoSessions_ShouldCreateDifferentPlayers()
    {
        DebugExtension.Log(this, "Starting..");
        var s1 = CreateGameSession();
        var s2 = CreateGameSession();
        Assert.NotEqual(s1.Player.Id, s2.Player.Id);
    }
}
