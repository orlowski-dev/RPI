using Xunit;

public class EnemyCharacterTest
{
    [Fact]
    public void CreationTest()
    {
        var enemy = new EnemyCharacter(
            name: "Test Enemy",
            maxHp: 100,
            attack: 50,
            defense: 30,
            critChance: 1,
            level: 3,
            enemyType: EnemyType.Normal
        );

        Assert.Equal("Test Enemy", enemy.Name);
        Assert.Equal(3, enemy.Level);
    }
}
