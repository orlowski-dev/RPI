using Godot;

public partial class CombatData : GodotObject
{
    // public bool PlayerTurn { get; init; }
    public CombatTurn Turn { get; init; }
    public PlayerCharacter PlayerCharacter { get; init; }

    // public EnemyCharacter[] Enemies { get; init; }
    public EnemyCharacter Enemy { get; init; }

    public CombatData(CombatTurn turn, PlayerCharacter playerCharacter, EnemyCharacter enemy)
    {
        // PlayerTurn = playerTurn;
        // Enemies = enemies;
        Turn = turn;
        PlayerCharacter = playerCharacter;
        Enemy = enemy;
    }
}
