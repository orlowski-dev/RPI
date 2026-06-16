namespace Game.Tests.Actors;

public class PlayerTests
{
    private static Player GetPlayer(PlayerType? type = null)
    {
        return new("player", new(10, 10, 10, 10, 10), type: type ?? PlayerType.Warrior);
    }

    [Fact]
    public void Player_ShouldIncreaseExp()
    {
        Log.Write(this, "Starting..");
        var player = GetPlayer();
        var prevExp = player.Exp;
        Assert.Equal(0, prevExp);
        player.AddExperience(50);
        Assert.Equal(50, player.Exp);
    }

    [Fact]
    public void Player_ShouldLevelUp()
    {
        Log.Write(this, "Starting..");
        var player = GetPlayer();
        var expToLvl2 = 100;
        // 100 * Level^1.5
        Assert.Equal(expToLvl2, player.ExpNextLevel);
        player.AddExperience(expToLvl2);
        Assert.Equal(0, player.Exp);
        Assert.Equal(2, player.Level);
        Assert.Equal(282, player.ExpNextLevel);
    }

    [Fact]
    public void Player_LevelUpShouldIncreaseStats()
    {
        Log.Write(this, "Starting..");

        var player = GetPlayer(PlayerType.Warrior);
        var startMaxHp = player.Stats.MaxHp;
        var damege = 5;
        Assert.Equal(startMaxHp, player.Stats.MaxHp);
        player.ReceiveDamage(damege);
        Assert.Equal(startMaxHp - damege, player.Stats.CurrentHp);
        player.AddExperience(200);
        Assert.Equal(2, player.Level);
        // maxHp: Stats.MaxHp + (Level * Stats.MaxHp) + map[Type].MaxHp,
        var mapV = PlayerProgressionMap.Values[player.Type];
        Assert.True(startMaxHp < player.Stats.MaxHp);
        Assert.Equal(player.Stats.CurrentHp, player.Stats.MaxHp); // przy level up healup
    }

    [Fact]
    public void Player_ShouldIncreaseManyLevelsAtOnce()
    {
        var player = GetPlayer();
        // 100 * Level^1.5 = > 519 (na 4lvl)
        // 100 + 282 + 519 = 901
        player.AddExperience(902);
        Assert.Equal(4, player.Level);
        // zostaje 1
        Assert.Equal(1, player.Exp);
    }
}
