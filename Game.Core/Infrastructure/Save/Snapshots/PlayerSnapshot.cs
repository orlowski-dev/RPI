namespace Game.Core.Infrastructure.Save.Snapshots;

public record PlayerSnapshot(Guid Id, string Name, PlayerType Type, ActorStats Stats, int Level);
