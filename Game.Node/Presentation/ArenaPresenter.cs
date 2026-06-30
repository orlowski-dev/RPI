public class ArenaPresenter
{
    private readonly StartCombatUseCase _scUseCase;
    private readonly IGameSessionProvider _gsProvider;
    private readonly ResolveTurnUseCase _resolveTurnUseCase;
    private readonly CombatStateMachine _stateMachine;

    public ArenaPresenter(
        StartCombatUseCase scUseCase,
        IGameSessionProvider gsProvider,
        ResolveTurnUseCase resolveTurnUseCase,
        CombatStateMachine stateMachine
    )
    {
        _scUseCase = scUseCase;
        _gsProvider = gsProvider;
        _resolveTurnUseCase = resolveTurnUseCase;
        _stateMachine = stateMachine;
    }

    private CombatSession Session =>
        _gsProvider.Current?.CombatSession ?? throw new Exception("Combat session is not set!");

    public ArenaOnViewReadyVM OnViewReady(Player player)
    {
        var session = _gsProvider.Current!;
        var encounter =
            session.PendingEncounter
            ?? throw new InvalidOperationException("No pending encounter set!");

        var res = _scUseCase.Execute(
            new StartCombatRequest(Player: player, Enemies: encounter.Enemies)
        );
        if (res.IsFailure)
        {
            DebugExtension.Fatal(this, res.Error!.Message);
        }
        return new(CombatSession: res.Value.CombatSession);
    }

    // public ArenaOnAttackVM OnPlayerAttackAction()
    // {
    //     var res = _resolveTurnUseCase.Execute(
    //         new ResolveTurnRequest(
    //             Session: Session,
    //             StateMachine: _stateMachine,
    //             Action: new AttackAction()
    //         )
    //     );

    //     if (res.IsFailure)
    //     {
    //         DebugExtension.Fatal(this, res.Error!.Message);
    //     }

    //     return new(res.Value);
    // }

    public ArenaOnAttackVM OnPlayerAttackAction()
    {
        Session.SelectAction(new AttackAction());
        return new(null!); // VM pusty - krokowanie obsłuży ArenaScene
    }

    public bool StepCombat()
    {
        var ctx = Session.Context;
        return _stateMachine.Step(ctx);
    }
}
