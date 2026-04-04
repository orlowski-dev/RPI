public partial class CombatService
{
    // public bool PlayerTurn { get; private set; }
    public CombatTurn Turn { get; private set; }

    public PlayerCharacter PlayerCharacter { get; init; }

    // public EnemyCharacter[] Enemies { get; private set; } // później
    public EnemyCharacter Enemy { get; init; }

    // public CombatService(EnemyCharacter[] enemies, bool playerTurn = true)
    // {
    //     Enemies = enemies;
    //     PlayerTurn = playerTurn;
    // }

    public CombatService(
        PlayerCharacter playerCharacter,
        EnemyCharacter enemy,
        CombatTurn turn = CombatTurn.Player
    )
    {
        PlayerCharacter = playerCharacter;
        Enemy = enemy;
        Turn = turn;
    }

    public void ChangeTurn()
    {
        Turn = Turn == CombatTurn.Player ? CombatTurn.Opponent : CombatTurn.Player;
    }

    public void Attack<A, O>(A attacker, O opponent)
        where A : BaseCharacter
        where O : BaseCharacter
    {
        // atakujący to gracz
        if (attacker.GetType() == typeof(PlayerCharacter)) { }
    }
}
