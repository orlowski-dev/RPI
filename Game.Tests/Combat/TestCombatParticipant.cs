public static class TestCombatParticipant
{
    public static CombatParticipant Player { get; } =
        new CombatParticipant(
            "player",
            CombatParticipantType.Player,
            "none",
            new(10, 10, 10, 2, 2)
        );
    public static CombatParticipant Enemy { get; } =
        new CombatParticipant("enemy", CombatParticipantType.Enemy, "none", new(10, 10, 10, 2, 2));
}
