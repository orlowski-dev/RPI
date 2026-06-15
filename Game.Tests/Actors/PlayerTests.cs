namespace Game.Tests.Actors;

public class PlayerTests
{
    private static Player GetPlayer()
    {
        return new("player", new(10, 10, 10, 10, 10));
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

        var player = GetPlayer();
    }
}
