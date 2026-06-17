global using Game.Core.Application.Combat.DTO;
global using Game.Core.Application.Combat.UseCases;
global using Game.Core.Application.Results;
global using Game.Core.Domain.Actors;
global using Game.Core.Domain.Actors.Requests;
global using Game.Core.Domain.Combat;
global using Game.Core.Domain.Combat.Actions;
global using Game.Core.Domain.Combat.Reward;
global using Game.Core.Domain.Combat.States;
global using Game.Core.Domain.Exploration;
global using Game.Core.Domain.Session;
global using Game.Core.Extensions;
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
            new(Player: new(Name: Guid.NewGuid().ToString(), Type: Player.Type))
        );

        return session;
    }
}
