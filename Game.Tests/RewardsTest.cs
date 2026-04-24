// using Xunit;
// using Xunit.Abstractions;
// using Game.Tests.Shared;
// using Game.Core;
// using Game.Core.Data;
// using Game.Core.Services;

// public class RewardsTest
// {
//     private readonly ITestOutputHelper _output;

//     public RewardsTest(ITestOutputHelper output)
//     {
//         _output = output;
//     }

//     [Fact]
//     public void TestRealPlayerFightsGoblinAndPrintsFullStats()
//     {
//         var player = Shared.GetNewPlayer();

//         var rewardDb = new EnemyRewardsDB();
//         var rewardService = new RewardService(rewardDb);

//         var goblin = new EnemyCharacter(
//             name: "goblin",
//             maxHp: 30,
//             attack: 6,
//             defense: 2,
//             critChance: 5,
//             enemyType: EnemyType.Normal,
//             level: 1
//         );

//         var combat = new CombatService(
//             playerCharacter: player,
//             enemy: goblin,
//             rewardService: rewardService
//         );

//         _output.WriteLine("========================================");
//         _output.WriteLine("START TESTU - PRAWDZIWY GRACZ VS GOBLIN");
//         _output.WriteLine("========================================");

//         _output.WriteLine("");
//         _output.WriteLine("STATY GRACZA PRZED WALKĄ:");
//         PrintPlayerStats(player);

//         _output.WriteLine("");
//         _output.WriteLine("STATY GOBLINA PRZED WALKĄ:");
//         PrintEnemyStats(goblin);

//         int round = 1;

//         while (!combat.CombatEnded)
//         {
//             _output.WriteLine("");
//             _output.WriteLine($"---------- TURA {round} ----------");
//             _output.WriteLine($"Stan na start tury -> Gracz HP: {player.HP}/{player.MaxHP}, Goblin HP: {goblin.HP}/{goblin.MaxHP}");

//             var damageToEnemy = combat.Attack(player, goblin);
//             _output.WriteLine($"Gracz atakuje goblina za {damageToEnemy} dmg.");
//             _output.WriteLine($"Goblin po ataku -> HP: {goblin.HP}/{goblin.MaxHP}");

//             if (combat.CombatEnded)
//             {
//                 _output.WriteLine("Walka zakończyła się po ruchu gracza.");
//                 break;
//             }

//             var damageToPlayer = combat.Attack(goblin, player);
//             _output.WriteLine($"Goblin atakuje gracza za {damageToPlayer} dmg.");
//             _output.WriteLine($"Gracz po ataku -> HP: {player.HP}/{player.MaxHP}");

//             if (combat.CombatEnded)
//             {
//                 _output.WriteLine("Walka zakończyła się po ruchu goblina.");
//                 break;
//             }

//             round++;
//         }

//         _output.WriteLine("");
//         _output.WriteLine("WYNIK WALKI:");
//         _output.WriteLine($"- Stan walki: {combat.State}");
//         _output.WriteLine($"- Czy walka zakończona: {combat.CombatEnded}");

//         _output.WriteLine("");
//         _output.WriteLine("STATY GOBLINA PO WALCE:");
//         PrintEnemyStats(goblin);

//         _output.WriteLine("");
//         _output.WriteLine("STATY GRACZA PO WALCE:");
//         PrintPlayerStats(player);

//         Assert.True(combat.CombatEnded);
//     }

//     private void PrintPlayerStats(PlayerCharacter player)
//     {
//         _output.WriteLine($"- Name: {player.Name}");
//         _output.WriteLine($"- HP: {player.HP}/{player.MaxHP}");
//         _output.WriteLine($"- Attack: {player.Attack}");
//         _output.WriteLine($"- Defense: {player.Defense}");
//         _output.WriteLine($"- CritChance: {player.CritChance}");
//         _output.WriteLine($"- Luck: {player.Luck}");
//         _output.WriteLine($"- Gold: {player.Gold}");
//         _output.WriteLine($"- Exp: {player.Exp}");
//         _output.WriteLine($"- Level: {player.Level}");
//     }

//     private void PrintEnemyStats(EnemyCharacter enemy)
//     {
//         _output.WriteLine($"- Name: {enemy.Name}");
//         _output.WriteLine($"- HP: {enemy.HP}/{enemy.MaxHP}");
//         _output.WriteLine($"- Attack: {enemy.Attack}");
//         _output.WriteLine($"- Defense: {enemy.Defense}");
//         _output.WriteLine($"- CritChance: {enemy.CritChance}");
//         _output.WriteLine($"- Level: {enemy.Level}");
//         _output.WriteLine($"- EnemyType: {enemy.EnemyType}");
//     }
// }
