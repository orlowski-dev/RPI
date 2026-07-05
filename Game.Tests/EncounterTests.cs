public class EncounterTests
{
    public static Encounter GetNewEncounter()
    {
        var tcp = new TestCombatParticipant();
        return new([tcp.Enemy1, tcp.Enemy2]);
    }

    [Fact]
    [LogTest]
    public void Encounter_ShouldStart()
    {
        var enc = GetNewEncounter();
        Assert.Equal(EncounterState.Available, enc.State);
        enc.Start();
        Assert.Equal(EncounterState.InProgress, enc.State);
    }

    [Fact]
    [LogTest]
    public void Encounter_ShouldBecomeCleared()
    {
        var enc = GetNewEncounter();
    }
}
