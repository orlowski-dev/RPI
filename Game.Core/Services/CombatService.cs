public partial class CombatService
{
    public bool PlayerTurn { get; private set; }
    public EnemyCharacter[] Enemies { get; private set; }

    public CombatService(EnemyCharacter[] enemies, bool playerTurn = true)
    {
        Enemies = enemies;
        PlayerTurn = playerTurn;
    }

    public void ChangeTurn()
    {
        PlayerTurn = !PlayerTurn;
    }
}
