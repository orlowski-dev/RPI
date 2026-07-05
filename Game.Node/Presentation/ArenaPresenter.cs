public class ArenaPresenter
{
    private readonly StartCombatUseCase _scUseCase;
    private readonly IGameSessionProvider _gsProvider;
    private readonly ResolveTurnUseCase _resolveTurnUseCase;
    private readonly CombatStateMachine _stateMachine;
    private readonly FinishCombatUseCase _finishCombatUseCase;

    public ArenaPresenter(
        StartCombatUseCase scUseCase,
        IGameSessionProvider gsProvider,
        ResolveTurnUseCase resolveTurnUseCase,
        CombatStateMachine stateMachine,
        FinishCombatUseCase finishCombatUseCase
    )
    {
        _scUseCase = scUseCase;
        _gsProvider = gsProvider;
        _resolveTurnUseCase = resolveTurnUseCase;
        _stateMachine = stateMachine;
        _finishCombatUseCase = finishCombatUseCase;
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

    public ArenaOnCombatFinishedVM OnCombatFinished()
    {
        var res = _finishCombatUseCase.Execute(new FinishCombatRequest());
        if (res.IsFailure)
        {
            DebugExtension.Fatal(this, "Cannot get finish combat usecase response!");
        }

        return new(res.Value.Reward);
    }
}
