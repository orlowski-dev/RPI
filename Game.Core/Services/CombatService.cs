public partial class CombatService
{
    public CombatState State { get; private set; }

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
        bool playerBegins = true
    )
    {
        PlayerCharacter = playerCharacter;
        Enemy = enemy;
        State = playerBegins ? CombatState.PlayerMove : CombatState.EnemyMove;
    }

    public void ChangeTurn()
    {
        State = State == CombatState.PlayerMove ? CombatState.EnemyMove : CombatState.PlayerMove;
    }

    public int Attack<A, E>(A attacker, E enemy, bool defenseAction = false, bool crit = false)
        where A : BaseCharacter
        where E : BaseCharacter
    {
        var damage = defenseAction ? attacker.Attack * .5 : attacker.Attack;
        damage -= (enemy.Defense * .5);

        if (crit)
        {
            damage *= 1.5;
        }

        enemy.TakeDamage((int)damage);

        return (int)damage;
    }
}
