public class EncounterMapper : IEncounterSnapshotMapper
{
    private EnemyMapper _enemy;

    public EncounterMapper()
    {
        _enemy = new EnemyMapper();
    }

    public EncounterSnapshot ToSnapshot(Encounter encounter)
    {
        return new(
            Id: encounter.Id,
            State: encounter.State,
            Enemies: encounter.Enemies.Select((e) => _enemy.ToSnapshot(e)).ToList()
        );
    }

    public Encounter Restore(EncounterSnapshot encounterSnapshot)
    {
        return new(
            id: encounterSnapshot.Id,
            state: encounterSnapshot.State,
            enemies: encounterSnapshot.Enemies.Select((e) => _enemy.Restore(e)).ToList()
        );
    }
}
