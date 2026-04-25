using Game.Tests.Shared;

public class CombatServiceTest
{
    public (PlayerCharacter, EnemyCharacter, CombatService) InitData()
    {
        var player = Shared.GetNewPlayer();
        var enemy = Shared.GetNewEnemyCharacter();
        var service = new CombatService(playerCharacter: player, enemy: enemy);
        return (player, enemy, service);
    }

    [Fact]
    public void TestChangingTurn()
    {
        var (player, enemy, service) = InitData();

        Assert.Equal(CombatState.PlayerMove, service.State);
        service.ChangeTurn();
        Assert.Equal(CombatState.EnemyMove, service.State);
    }

    [Fact]
    public void TestPlayerAttackEnemy()
    {
        var (player, enemy, service) = InitData();
        var damage = service.Attack(player, enemy);

        Assert.Equal(enemy.MaxHP - damage, enemy.HP);
    }

    [Fact]
    public void TestEnemyAttackPlayer()
    {
        var (player, enemy, service) = InitData();
        var damage = service.Attack(enemy, player);

        Assert.Equal(player.MaxHP - damage, player.HP);
    }

    [Fact]
    public void TestPlayerAttactCrit()
    {
        var (player, enemy, service) = InitData();
        var enemy2 = Shared.GetNewEnemyCharacter();
        var damageWith = service.Attack(player, enemy, crit: true);
        var damageWithout = service.Attack(player, enemy2);

        Assert.NotEqual(enemy.HP, enemy2.HP);
        Assert.True(damageWith > damageWithout);
    }

    [Fact]
    public void TestAttackSmallerThanDefense()
    {
        var player = new PlayerCharacter(
            name: "Test Character",
            maxHp: 10,
            attack: 1,
            defense: 10,
            luck: 1,
            critChance: 1,
            characterClass: new CharacterClass(
                name: "Warrior",
                hpBase: 140,
                attackBase: 12,
                defenseBase: 10,
                critBase: 5,
                luckBase: 2,
                hpBonus: 20,
                attackBonus: 3,
                defenseBonus: 3
            )
        );
        var enemy = new EnemyCharacter(
            name: "Test Enemy",
            maxHp: 10,
            attack: 1,
            defense: 1,
            critChance: 1,
            level: 3,
            enemyType: EnemyType.Normal
        );

        var service = new CombatService(player, enemy);

        var initPlayerHp = player.HP;
        var damage = service.Attack(enemy, player); // -4

        // jak defense większy niż atak przeciwnika to hp powinno być takie samo jak na początku
        Assert.Equal(initPlayerHp, player.HP);
    }
}
