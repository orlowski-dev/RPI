using System.Text.Json;
using Game.Core.Application.Save.Requests;
using Game.Core.Application.Save.UseCases;
using Game.Core.Infrastructure.Save.Mapping;
using Game.Core.Infrastructure.Save.Repositories;

namespace Game.Tests.Infrastructure;

public class SaveTests
{
    [Fact]
    public void ToSnapshot_ShouldMapPlayer()
    {
        DebugExtension.Log(this, "Starting..");

        var session = Globals.CreateGameSession();
        // var snapshot = new SnapshotMapper().ToSnapshot(session);
        var snapshot = new GameSnapshotAssembler().ToSnapshot(session);

        Assert.Equal(session.Player.Id, snapshot.Player.Id);
        Assert.Equal(session.Player.Name, snapshot.Player.Name);
        Assert.Equal(session.Player.Type, snapshot.Player.Type);
    }

    [Fact]
    public void SaveGame_ShouldCreateJson()
    {
        DebugExtension.Log(this, "Starting..");

        var saveUC = new SaveGameUseCase();
        var gameSession = Globals.CreateGameSession();
        Assert.NotNull(gameSession.Player);
        var saveReq = new SaveGameRequest(gameSession);
        var saveUCRes = saveUC.Execute(saveReq);
        Assert.True(saveUCRes.IsSuccess);
    }

    [Fact]
    public void PlayerMapper_ToSnapshot_ShouldReturnSnapshot()
    {
        DebugExtension.Log(this, "Starting..");

        var player = Globals.Player;
        var playerMapper = new PlayerMapper();
        var playerSnapshot = playerMapper.ToSnapshot(player);
        Assert.NotNull(playerSnapshot);
        Assert.Equal(player.Id, playerSnapshot.Id);
    }

    [Fact]
    public void GameSnapshotAssembler_ToSnapshot()
    {
        DebugExtension.Log(this, "Starting..");
        var session = Globals.CreateGameSession();
        var gameSnapshot = new GameSnapshotAssembler().ToSnapshot(session);
        Assert.NotNull(gameSnapshot);
        Assert.NotNull(gameSnapshot.Player);
    }

    [Fact]
    public void GameSnapshot_ShouldSerialize_Player()
    {
        DebugExtension.Log(this, "Starting..");
        var session = Globals.CreateGameSession();

        var snapshot = new GameSnapshotAssembler().ToSnapshot(session);

        var json = JsonSerializer.Serialize(snapshot);

        Assert.Contains("\"Player\"", json);
        Assert.DoesNotContain("\"Player\":{}", json);
    }

    [Fact]
    public void Save_ShouldContainPlayer()
    {
        DebugExtension.Log(this, "Starting..");

        var session = Globals.CreateGameSession();
        var snapshot = new GameSnapshotAssembler().ToSnapshot(session);
        new JsonSaveRepository().Save(snapshot);
        var json = File.ReadAllText($"save/{snapshot.Id}.json");

        Assert.Contains("\"Player\"", json);
        Assert.DoesNotContain("\"Player\":{}", json);
    }

    [Fact]
    public void Save_ShouldReturnResultOnLoad()
    {
        DebugExtension.Log(this, "Starting..");

        var session = Globals.CreateGameSession();
        var snapshot = new GameSnapshotAssembler().ToSnapshot(session);
        new JsonSaveRepository().Save(snapshot);

        var loaded = new JsonSaveRepository().Load(snapshot.Id);
        Assert.True(loaded.IsSuccess);
        Assert.Equivalent(snapshot, loaded.Value);
    }

    // [Fact]
    // public void Save_ListShouldReturnGameSnapshots()
    // {
    //     DebugExtension.Log(this, "Starting..");

    //     var s1 = Globals.CreateGameSession();
    //     var snap1 = new GameSnapshotAssembler().ToSnapshot(s1);
    //     new JsonSaveRepository().Save(snap1);

    //     var s2 = Globals.CreateGameSession();
    //     var snap2 = new GameSnapshotAssembler().ToSnapshot(s2);
    //     new JsonSaveRepository().Save(snap2);

    //     var saves = new JsonSaveRepository().List();
    //     Assert.NotEmpty(saves);
    // }
}
