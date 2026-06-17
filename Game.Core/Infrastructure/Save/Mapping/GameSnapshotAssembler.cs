using Game.Core.Domain.Session;
using Game.Core.Infrastructure.Save.Contracts;
using Game.Core.Infrastructure.Save.Snapshots;

namespace Game.Core.Infrastructure.Save.Mapping;

public class GameSnapshotAssembler
{
    private IPlayerSnapshotMapper _player;
    private IDungeonSnapshotMapper _dungeon;

    public GameSnapshotAssembler(IPlayerSnapshotMapper player, IDungeonSnapshotMapper dungeon)
    {
        _player = player;
        _dungeon = dungeon;
    }

    public GameSnapshot ToSnapshot(GameSession gameSession)
    {
        return new(
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
