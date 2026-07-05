public class PlayerDeathState : ICombatState
{
    public CombatStateType Type { get; } = CombatStateType.PlayerDeath;

    public bool ReturnsControlToUi { get; } = true;

    // wywoływane raz - wchodzę do stanu np. tura gracza się zaczęła
    public void Enter(CombatContext ctx) { }

    // wywołuje się wiele razy - czy gracz JUŻ wykonał akcję? co potem?
    public CombatStateTransition Update(CombatContext ctx)
    {
        return CombatStateTransition.Stay();
    }

    // wywoływane raz - sprzątanie po stanie - np. kończę turę
    public void Exit(CombatContext ctx) { }
}
