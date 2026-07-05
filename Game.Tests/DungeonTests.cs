public class DungeonTests
{
    [Fact]
    [LogTest]
    public void Dungeon_ShouldBeenCreated()
    {
        var dung = new DungeonFactory().Create();
        Assert.NotNull(dung);
    }
}
