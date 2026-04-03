using Godot;

public partial class CombatData : GodotObject
{
    public bool PlayerTurn { get; init; }
    public EnemyCharacter[] Enemies { get; init; }

    public CombatData(bool playerTurn, EnemyCharacter[] enemies)
    {
        PlayerTurn = playerTurn;
        Enemies = enemies;
    }
}
