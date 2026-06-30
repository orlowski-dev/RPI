public class CombatStateMachine
{
    private readonly Dictionary<CombatStateType, ICombatState> _states;
    private ICombatState? _currentState;

    public CombatStateType CurrentState => _currentState?.Type ?? CombatStateType.Start;

    public CombatStateMachine(IEnumerable<ICombatState> states)
    {
        _states = states.ToDictionary(x => x.Type);
    }

    public void Start(CombatContext ctx)
    {
        Change(CombatStateType.PlayerTurn, ctx);
    }

    // pełna sekwencja - zostaje dla testów, bez zmian
    public void Update(CombatContext context)
    {
        if (_currentState is null)
        {
            DebugExtension.Fatal(this, "Combat state is not initialized.");
        }

        while (true)
        {
            var transition = _currentState.Update(context);

            if (!transition.ShouldChange)
            {
                return;
            }

            HandleTransition(transition, context);

            if (_currentState.ReturnsControlToUi)
            {
                return;
            }
        }
    }

    /// <summary>
    /// Wykonuje pojedynczy krok maszyny stanów.
    /// Zwraca true jeśli jest jeszcze coś do zrobienia automatycznie,
    /// false jeśli oddano kontrolę do UI (np. czekamy na gracza).
    /// </summary>
    public bool Step(CombatContext context)
    {
        if (_currentState is null)
        {
            DebugExtension.Fatal(this, "Combat state is not initialized.");
        }

        var transition = _currentState.Update(context);

        if (!transition.ShouldChange)
        {
            return false;
        }

        HandleTransition(transition, context);

        return !_currentState.ReturnsControlToUi;
    }

    private void HandleTransition(CombatStateTransition transition, CombatContext context)
    {
        if (!transition.ShouldChange)
        {
            return;
        }

        if (transition.NextState is null)
        {
            DebugExtension.Fatal(this, "Transition requested state change but next state is null.");
        }

        Change(transition.NextState.Value, context);
    }

    private void Change(CombatStateType next, CombatContext ctx)
    {
        _currentState?.Exit(ctx);
        _currentState = _states[next];
        ctx.Session.State = next;
        _currentState.Enter(ctx);
    }
}
