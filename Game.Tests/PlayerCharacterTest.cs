using Xunit;

public class PlayerCharacterTest
{
    [Fact]
    public void CreationTest()
    {
        var player = new PlayerCharacter(
            name: "Test Character",
            maxHp: 100,
            attack: 50,
            defense: 30,
            luck: 10,
            critChance: 1
        );

        Assert.Equal("Test Character", player.Name);
    }
}
