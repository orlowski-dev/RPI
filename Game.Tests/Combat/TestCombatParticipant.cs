public class TestCombatParticipant
{
    public CombatParticipant Player { get; } =
        new CombatParticipant(
            "player",
            CombatParticipantType.Player,
            "none",
            new(30, 10, 10, 2, 2)
        );
    public CombatParticipant Enemy1 { get; } =
        new CombatParticipant(
            "enemy_1",
            CombatParticipantType.Enemy,
            "none",
            new(20, 10, 10, 2, 2)
        );

    public CombatParticipant Enemy2 { get; } =
        new CombatParticipant(
            "enemy_2",
            CombatParticipantType.Enemy,
            "none",
            new(10, 10, 10, 2, 2)
        );
}
