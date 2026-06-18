// co dzieje się w świecie gry
public record DungeonSnapshot(Guid Id, IReadOnlyList<EncounterSnapshot> Encounters);
