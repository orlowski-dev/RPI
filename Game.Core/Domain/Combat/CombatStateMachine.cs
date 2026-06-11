namespace Game.Core.Domain.Combat;

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

    public void Update(CombatContext context)
    {
        if (_currentState is null)
        {
            throw new InvalidOperationException("Combat state is not initialized.");
        }

        var transition = _currentState.Update(context);

        HandleTransition(transition, context);
    }

    private void HandleTransition(CombatStateTransition transition, CombatContext context)
    {
        // zostań tutaj :(
        if (!transition.ShouldChange)
        {
            return;
        }

        if (transition.NextState is null)
        {
            throw new InvalidOperationException(
                "Transition requested state change but next state is null."
            );
        }

        Change(transition.NextState.Value, context);
    }

    // stary stan na nowy stano - co robić po wejsciu
    private void Change(CombatStateType next, CombatContext ctx)
    {
        // opuszczam poprzedni stan
        _currentState?.Exit(ctx);
        //pobiream nowy stan
        _currentState = _states[next];
        // aktualizuje stan sesji
        ctx.Session.State = next;
        // przygotowuje stan
        _currentState.Enter(ctx);

        if (!_currentState.IsAutomatic)
        {
            return;
        }

        var transition = _currentState.Update(ctx);
        HandleTransition(transition, ctx);
    }
}
