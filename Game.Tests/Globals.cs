global using Xunit;

public static class Globals
{
    public static Player Player =>
        new(name: "Player", stats: new(30, 10, 5, 2, 3), type: PlayerType.Warrior);

    public static Enemy Enemy1 =>
        new(
            name: "Goblin",
            goldReward: 1,
            expReward: 1,
            stats: new(20, 5, 3, 2, 3),
            rank: EnemyRank.Normal,
            type: EnemyType.Goblin
        );

    public static Enemy Enemy2 =>
        new(
            name: "Ork",
            goldReward: 1,
            expReward: 1,
            stats: new(10, 3, 1, 2, 3),
            rank: EnemyRank.Normal,
            type: EnemyType.Ork
        );

    public static GameSession CreateGameSession()
    {
        var playerFactory = new PlayerFactory();

        var factory = new GameSessionFactory();

        var session = factory.Create(
            playerName: Guid.NewGuid().ToString(),
            playerType: PlayerType.Warrior
        );

        return session;
    }

    public static GameSessionProvider TestGetGameSessionProvider()
    {
        var sp = new GameSessionProvider();
        sp.Set(CreateGameSession());
        return sp;
    }
}
