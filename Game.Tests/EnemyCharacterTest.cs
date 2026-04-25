using Game.Tests.Shared;
using Xunit;

public class EnemyCharacterTest
{
    [Fact]
    public void CreationTest()
    {
        var enemy = Shared.GetNewEnemyCharacter();

        Assert.Equal("Test Enemy", enemy.Name);
        Assert.Equal(3, enemy.Level);
    }
}
