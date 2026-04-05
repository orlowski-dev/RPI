using Game.Tests.Shared;
using Xunit;

public class PlayerCharacterTest
{
    [Fact]
    public void CreationTest()
    {
        var player = Shared.GetNewPlayer();
        Assert.Equal("Test Character", player.Name);
        Assert.Equal(1, player.Level);
    }

    [Fact]
    public void ExpToNextLvlTest()
    {
        var player = Shared.GetNewPlayer();
        // początkowe expNextLevel
        Assert.Equal(100, player.ExpNextLvl);
        var lvl1Hp = player.HP;

        player.AddExp(100);

        // dodaję 100 exa i wbijam drugi level
        Assert.Equal(100, player.Exp);
        Assert.Equal(2, player.Level);
        Assert.Equal(lvl1Hp + player.CharacterClass.HpBonus, player.HP);

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
