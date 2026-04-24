// using Game.Tests.Shared;

// public class CombatServiceTest
// {
//     public (PlayerCharacter, EnemyCharacter, CombatService) InitData()
//     {
//         var player = Shared.GetNewPlayer();
//         var enemy = Shared.GetNewEnemyCharacter();
//         var service = new CombatService(playerCharacter: player, enemy: enemy);
//         return (player, enemy, service);
//     }

//     [Fact]
//     public void TestChangingTurn()
//     {
//         var (player, enemy, service) = InitData();

//         Assert.Equal(CombatState.PlayerMove, service.State);
//         service.ChangeTurn();
//         Assert.Equal(CombatState.EnemyMove, service.State);
//     }

//     [Fact]
//     public void TestPlayerAttackEnemy()
//     {
//         var (player, enemy, service) = InitData();
//         var damage = service.Attack(player, enemy);

//         Assert.Equal(enemy.MaxHP - damage, enemy.HP);
//     }

//     [Fact]
//     public void TestEnemyAttackPlayer()
//     {
//         var (player, enemy, service) = InitData();
//         var damage = service.Attack(enemy, player);

//         Assert.Equal(player.MaxHP - damage, player.HP);
//     }

//     [Fact]
//     public void TestPlayerAttactCrit()
//     {
//         var (player, enemy, service) = InitData();
//         var enemy2 = Shared.GetNewEnemyCharacter();
//         var damageWith = service.Attack(player, enemy, crit: true);
//         var damageWithout = service.Attack(player, enemy2);

//         Assert.NotEqual(enemy.HP, enemy2.HP);
//         Assert.True(damageWith > damageWithout);
//     }
// }
