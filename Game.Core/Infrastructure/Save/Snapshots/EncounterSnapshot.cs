public record EncounterSnapshot(
    Guid Id,
    EncounterState State,
    IReadOnlyList<EnemySnapshot> Enemies
);
