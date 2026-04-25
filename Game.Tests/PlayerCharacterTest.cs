using Game.Tests.Shared;

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
        Assert.Equal(lvl1Hp + player.CharacterClass.MaxHpBonus, player.HP);

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

    [Fact]
    public void TestHealOnLevelUp()
    {
        var player = Shared.GetNewPlayer();
        Assert.Equal(100, player.ExpNextLvl);
        player.TakeDamage(10);
        Assert.Equal(90, player.HP);
        player.AddExp(200);
        Assert.Equal(2, player.Level);
        Assert.Equal(120, player.MaxHP);
        Assert.Equal(120, player.HP);

        // 6 poziom
        player.AddExp(1200);
        Assert.Equal(6, player.Level);
        Assert.Equal(200, player.MaxHP);
        player.TakeDamage(120);
        Assert.Equal(80, player.HP);
        player.AddExp(300);
        Assert.Equal(7, player.Level);
        Assert.Equal(220, player.MaxHP);
        Assert.Equal(220, player.HP);

        // var cc = new CharacterClass(
        //     name: "Warrior",
        //     hpBase: 100,
        //     attackBase: 50,
        //     defenseBase: 30,
        //     critBase: 1,
        //     luckBase: 10,
        //     maxHpBonus: 20,
        //     attackBonus: 3,
        //     defenseBonus: 3
        // );
        // return new PlayerCharacter(
        //     name: "Test Character",
        //     maxHp: cc.HpBase,
        //     attack: cc.AttackBase,
        //     defense: cc.AttackBase,
        //     luck: cc.AttackBase,
        //     critChance: cc.CritBase,
        //     characterClass: cc
        // );
    }
}
