using Xunit;

public class PlayerCharacterTest
{
    public PlayerCharacter GetNewPlayer()
    {
        return new PlayerCharacter(
            name: "Test Character",
            maxHp: 100,
            attack: 50,
            defense: 30,
            luck: 10,
            critChance: 1
        );
    }

    [Fact]
    public void CreationTest()
    {
        var player = GetNewPlayer();
        Assert.Equal("Test Character", player.Name);
        Assert.Equal(1, player.Level);
    }

    [Fact]
    public void ExpToNextLvlTest()
    {
        var player = GetNewPlayer();
        // początkowe expNextLevel
        Assert.Equal(100, player.ExpNextLvl);

        player.AddExp(100);

        // dodaję 100 exa i wbijam drugi level
        Assert.Equal(100, player.Exp);
        Assert.Equal(2, player.Level);

        // sprawdzam czy expNextLvl = 282 - wdł tabelki
        Assert.Equal(282, player.ExpNextLvl);

        // dodaję 200 expa
        player.AddExp(200);
        Assert.Equal(300, player.Exp);

        // lvl powinien być 3
        Assert.Equal(3, player.Level);

        // epxNextLvl 519
        Assert.Equal(519, player.ExpNextLvl);

        // dodaję exp na 6 lvl - sprawdzam lvlowanie o 3 poziomy
        player.AddExp(1000);
        Assert.Equal(6, player.Level);
        Assert.Equal(1469, player.ExpNextLvl);
    }
}
