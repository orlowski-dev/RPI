public class GameSnapshotAssembler
{
    private IPlayerSnapshotMapper _player;
    private IDungeonSnapshotMapper _dungeon;

    public GameSnapshotAssembler()
    {
        _player = new PlayerMapper();
        _dungeon = new DungeonMapper();
    }

    public GameSnapshot ToSnapshot(GameSession gameSession)
    {
        // DebugExtension.Log(this, $"gameSession:Player {DebugExtension.Dump(gameSession.Player)}");
        // DebugExtension.Log(
        //     this,
        //     $"snapshot:player {DebugExtension.Dump(_player.ToSnapshot(gameSession.Player))}"
        // );
        return new(
            CreatedAt: DateTime.Now,
            Id: gameSession.Id,
            State: gameSession.State,
            Player: _player.ToSnapshot(gameSession.Player),
            Dungeon: _dungeon.ToSnapshot(gameSession.Dungeon)
        );
    }

    public GameSession Restore(GameSnapshot snapshot)
    {
        return new(
            id: snapshot.Id,
            state: snapshot.State,
            player: _player.Restore(snapshot.Player),
            dungeon: _dungeon.Restore(snapshot.Dungeon)
        );
    }
}
