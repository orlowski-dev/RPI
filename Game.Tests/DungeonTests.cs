public class DungeonTests
{
    [Fact]
    public void Dungeon_ShouldBeenCreated()
    {
        DebugExtension.Log(this, "Started..");
        var dung = new DungeonFactory().Create();
        Assert.NotNull(dung);
    }
}
