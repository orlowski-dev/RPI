using Game.Core.Infrastructure.Save.Mapping;

namespace Game.Tests.Infrastructure;

public class SaveTests
{
    [Fact]
    public void ToSnapshot_ShouldMapPlayer()
    {
        DebugExtension.Log(this, "Starting..");

        var session = Globals.CreateGameSession();
        var snapshot = new SnapshotMapper().ToSnapshot(session);

        Assert.Equal(session.Player.Id, snapshot.Player.Id);
        Assert.Equal(session.Player.Name, snapshot.Player.Name);
        Assert.Equal(session.Player.Type, snapshot.Player.Type);
    }
}
