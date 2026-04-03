public partial class CombatService
{
    public bool PlayerTurn { get; private set; }

    public CombatService(bool playerTurn = true)
    {
        PlayerTurn = playerTurn;
    }

    public void ChangeTurn()
    {
        PlayerTurn = !PlayerTurn;
    }
}
