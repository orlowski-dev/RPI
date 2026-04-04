using Godot;

public partial class CombatData : GodotObject
{
    public CombatState State { get; init; }
    public PlayerCharacter PlayerCharacter { get; init; }

    // public EnemyCharacter[] Enemies { get; init; }
    public EnemyCharacter Enemy { get; init; }

    public CombatData(CombatState state, PlayerCharacter playerCharacter, EnemyCharacter enemy)
    {
        State = state;
        PlayerCharacter = playerCharacter;
        Enemy = enemy;
    }
}
