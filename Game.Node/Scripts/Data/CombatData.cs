using Godot;

/// <summary>
/// Klasa DTO przechowującą dane potrzebne do:
/// aktualizacji UI walki,
/// synchronizacji stanu walki,
/// komunikacji CombatController → CombatHUD.
/// </summary>
/// <param name=""></param>
/// <remarks>
/// Klasa nie zawiera logiki.
/// </remarks>
public partial class CombatData : GodotObject
{
    /// <summary>
    /// Aktualny stan walki
    /// </summary>
    public CombatState State { get; init; }

    /// <summary>
    /// Statystyki postaci gracza
    /// </summary>
    public PlayerCharacter PlayerCharacter { get; init; }

    // public EnemyCharacter[] Enemies { get; init; }
    public EnemyCharacter Enemy { get; init; }

    public CombatData(
        CombatState state,
        PlayerCharacter playerCharacter = null,
        EnemyCharacter enemy = null
    )
    {
        State = state;
        PlayerCharacter = playerCharacter;
        Enemy = enemy;
    }
}
