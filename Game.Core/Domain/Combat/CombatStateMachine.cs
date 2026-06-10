namespace Game.Core.Domain.Combat;

public partial class CombatStateMachine
{
    private readonly Dictionary<CombatStateType, ICombatState> _states;
    private ICombatState? _current;

    public CombatStateType Current => _current?.Type ?? CombatStateType.Start;

    public CombatStateMachine(IEnumerable<ICombatState> states)
    {
        _states = states.ToDictionary(x => x.Type);
    }

    public void Start(CombatContext ctx) { }

    public void Update(CombatContext ctx)
    {
        if (_current is null)
        {
            throw new InvalidOperationException("State machine not started");
        }

        var transition = _current.Update(ctx);

        if (!transition.ShouldChange)
        {
            return;
        }
    }

    private void Change(CombatStateType next, CombatContext ctx)
    {
        _current?.Exit(ctx);
        _current = _states[next];
        _current.Enter(ctx);
    }
}
