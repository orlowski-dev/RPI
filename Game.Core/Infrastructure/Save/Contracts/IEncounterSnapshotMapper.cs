public interface IEncounterSnapshotMapper
{
    EncounterSnapshot ToSnapshot(Encounter encounter);
    Encounter Restore(EncounterSnapshot encounterSnapshot);
}
