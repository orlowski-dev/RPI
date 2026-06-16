using Game.Tests.Combat;

namespace Game.Tests.Exploration;

public class EncounterTests
{
    public static Encounter GetNewEncounter()
    {
        var tcp = new TestCombatParticipant();
        return new([tcp.Enemy1, tcp.Enemy2]);
    }

    [Fact]
    public void Encounter_ShouldStart()
    {
        DebugExtension.Log(this, "Started..");

        var enc = GetNewEncounter();
        Assert.Equal(EncounterState.Available, enc.State);
        enc.Start();
        Assert.Equal(EncounterState.InProgress, enc.State);
    }

    [Fact]
    public void Encounter_ShouldBecomeCleared()
    {
        DebugExtension.Log(this, "Started..");

        var enc = GetNewEncounter();
    }
}
