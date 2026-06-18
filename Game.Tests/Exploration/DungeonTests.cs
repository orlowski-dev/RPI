public class DungeonTests
{
    [Fact]
    public void Dungeon_ShouldBeenCreated()
    {
        DebugExtension.Log(this, "Started..");
        var dung = new Dungeon();
        Assert.NotNull(dung);
    }
}
