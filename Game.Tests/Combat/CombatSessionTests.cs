public class CombatSessionTests
{
    private (List<CombatParticipant>, CombatSession) CreateSession()
    {
        var tcp = new TestCombatParticipant();
        var player = tcp.Player;
        var enemy1 = tcp.Enemy1;
        var enemy2 = tcp.Enemy2;

        var session = new CombatSession(new[] { player, enemy1, enemy2 });

        return (new() { player, enemy1, enemy2 }, session);
    }

    [Fact]
    public void CombatFlow_ShouldFinishCombat()
    {
        Console.WriteLine($"[Test]: {this.GetType().Name}");
        var tcp = new TestCombatParticipant();
        var player = tcp.Player;
        var enemy1 = tcp.Enemy1;
        var enemy2 = tcp.Enemy2;

        var start = new StartCombatUseCase();
        var combat = start.Execute(new(player, new[] { enemy1, enemy2 }));

        var resolve = new ResolveTurnUseCase();
        CombatTurnResultDto dto = default!;

        var maxIterations = 10;
        var iterations = 0;
        var end = false;

        while (iterations++ < maxIterations && !end)
        {
            var session = combat.Value.CombatSession;

            if (session.State == CombatStateType.PlayerTurn)
            {
                var target = session.AliveEnemies.FirstOrDefault();
                if (target is null)
                {
                    end = true;
                }
                session.SetTarget(target);
                dto = resolve
                    .Execute(
                        new(
                            Session: session,
                            StateMachine: combat.Value.StateMachine,
                            Action: new AttackAction()
                        )
                    )
                    .Value;
                Console.WriteLine(
                    $"{session.ActiveParticipant.Id} atakuje {session.Target!.Id}. Next participant is: {session.NextParticipant!.Id}"
                );
            }
            else
            {
                dto = resolve
                    .Execute(new(Session: session, StateMachine: combat.Value.StateMachine))
                    .Value;
            }

            Console.WriteLine(DebugExtension.Dump(dto));

            if (dto.CombatFinished)
                break;

            Assert.True(iterations < maxIterations);
        }
    }

    // [Fact]
    // public void CombatFlow_PlayerAttack_ShouldReturnToPlayerTurn()
    // {
    //     Console.WriteLine("[?] Test: CombatFlow_PlayerAttack_ShouldReturnToPlayerTurn");
    //     var tcp = new TestCombatParticipant();
    //     var start = new StartCombatUseCase();
    //     var startResult = start.Execute(
    //         new StartCombatRequest(tcp.Player, new[] { tcp.Enemy1, tcp.Enemy2 })
    //     );
    //     var session = startResult.Value.CombatSession;
    //     var machine = startResult.Value.StateMachine;

    //     Assert.Equal(CombatStateType.PlayerTurn, session.State);

    //     var resolve = new ResolveTurnUseCase();
    //     var result = resolve.Execute(new ResolveTurnRequest(session, new AttackAction(), machine));

    //     Assert.True(result.IsSuccess);
    //     Assert.Equal(CombatStateType.PlayerTurn, session.State);
    // }

    // [Fact]
    // public void CombatFlow_PlayerAttack_ShouldDamageEnemy()
    // {
    //     Console.WriteLine("[?] Test: CombatFlow_PlayerAttack_ShouldDamageEnemy");
    //     var tcp = new TestCombatParticipant();
    //     var enemyStartHp = tcp.Enemy1.Stats.MaxHp;
    //     var start = new StartCombatUseCase();
    //     var combat = start.Execute(new(tcp.Player, new[] { tcp.Enemy1 }));

    //     // player manualnie wybiera taeget
    //     combat.Value.CombatSession.SetTarget(tcp.Enemy1);

    //     Assert.NotNull(combat.Value.CombatSession.Target);

    //     Assert.Equal(tcp.Enemy1.Id, combat.Value.CombatSession.Target!.Id);

    //     var resolve = new ResolveTurnUseCase();
    //     resolve.Execute(
    //         new(combat.Value.CombatSession, new AttackAction(), combat.Value.StateMachine)
    //     );
    //     Assert.True(enemyStartHp > tcp.Enemy1.CurrentHp);
    // }

    // [Fact]
    // public void CombatFlow_ShouldChangeState()
    // {
    //     Console.WriteLine("[?] Test: CombatFlow_ShouldChangeState");
    //     var machine = new CombatStateMachine(
    //         new ICombatState[] { new PlayerTurnState(), new EnemyTurnState() }
    //     );

    //     var (_, session) = CreateSession();
    //     machine.Start(session.Context);
    //     machine.Update(session.Context);
    //     Assert.NotEqual(CombatStateType.Start, session.State);
    // }

    // [Fact]
    // public void CombatFlow_PlayerAttack_ShouldTriggerEnemyTurns()
    // {
    //     Console.WriteLine("[?] Test: CombatFlow_PlayerAttack_ShouldTriggerEnemyTurns");
    //     var tcp = new TestCombatParticipant();
    //     var playerStartHp = tcp.Player.CurrentHp;
    //     var start = new StartCombatUseCase();
    //     var combat = start.Execute(new(tcp.Player, new[] { tcp.Enemy1, tcp.Enemy2 }));

    //     combat.Value.CombatSession.SetTarget(tcp.Enemy1);

    //     var resolve = new ResolveTurnUseCase();

    //     resolve.Execute(
    //         new(combat.Value.CombatSession, new AttackAction(), combat.Value.StateMachine)
    //     );

    //     Assert.True(tcp.Player.CurrentHp < playerStartHp);
    //     Assert.Equal(CombatStateType.PlayerTurn, combat.Value.CombatSession.State);
    // }

    // // sprawdzam czy CombatSession ustawia stan początkowy i wybiera pierwszego uczestnika
    // [Fact]
    // public void Constructor_ShouldInitializeSession()
    // {
    //     var (participants, session) = CreateSession();

    //     Assert.Equal(CombatStateType.Start, session.State);
    //     session.SetActiveParticipant(participants[0]);
    //     Assert.Equal(participants.First(), session.ActiveParticipant);
    //     Assert.False(session.IsFinished);
    //     Assert.Equal(1, session.TurnNumber);
    // }

    // // sprawdzam UI wykonuje akcje a CombatSession ją posiada
    // [Fact]
    // public void SelectAction_ShouldMarkActionAsSelected()
    // {
    //     var (_, session) = CreateSession();

    //     var action = new FakeAction();

    //     session.SelectAction(action);
    //     Assert.True(session.HasSelectedAction);
    // }

    // // sprawdzam czy akcja została wykonana
    // [Fact]
    // public void ExecuteSelectedAction_ShouldConsumeAction()
    // {
    //     var (_, session) = CreateSession();
    //     var action = new FakeAction();
    //     session.SelectAction(action);
    //     var result = session.ExecuteSelectedAction();
    //     Assert.True(result.IsSuccess);
    //     Assert.False(session.HasSelectedAction);
    //     Assert.True(action.Executed);
    // }

    // // sprawdzam flow walki player -> enemy1 -> enemy2 -> player
    // [Fact]
    // void CombatFlow_ShouldWorkAsExpected()
    // {
    //     var (participants, session) = CreateSession();
    //     var player = participants.First();
    //     var enemy1 = participants[1];
    //     var enemy2 = participants[2];

    //     // tura gracza na starcie
    //     Assert.Equal(CombatStateType.Start, session.State);
    //     session.SetActiveParticipant(player);
    //     Assert.Equal(CombatStateType.PlayerTurn, session.State);
    //     Assert.Equal(player, session.ActiveParticipant);
    //     Assert.Equal(1, session.TurnNumber);

    //     // gracz wykonuje akcję
    //     var action = new FakeAction();
    //     session.SelectAction(action);
    //     var result = session.ExecuteSelectedAction();
    //     Assert.True(result.IsSuccess);

    //     // przeciwnycy wykonują akcje

    //     foreach (var en in new[] { enemy1, enemy2 })
    //     {
    //         session.SetActiveParticipant(en);
    //         Assert.Equal(CombatStateType.EnemyTurn, session.State);
    //         Assert.Equal(en, session.ActiveParticipant);
    //         Assert.Equal(player, session.Target);
    //         Assert.Equal(1, session.TurnNumber); // wciąż tura 1 dopóki gracz nie ma znowu tury

    //         // przeciwnik atakuje gracza czyli -10hp
    //         var attack = new AttackAction();
    //         session.SelectAction(attack);
    //         session.ExecuteSelectedAction();
    //     }

    //     // hp po 2*10attack powinno być 10 u gracza
    //     Assert.Equal(10, player.CurrentHp);

    //     session.SetActiveParticipant(player);
    //     Assert.Equal(CombatStateType.PlayerTurn, session.State);
    //     Assert.Equal(player, session.ActiveParticipant);
    //     Assert.Null(session.Target);
    //     Assert.Equal(2, session.TurnNumber);

    //     // gracza atakuje enemy1 i go pokonuje bo attack=enemyCurrentHP
    //     session.SetTarget(enemy1);
    //     var attackAction = new AttackAction();
    //     session.SelectAction(attackAction);
    //     session.ExecuteSelectedAction();
    //     Assert.Equal(0, enemy1.CurrentHp);
    //     Assert.False(enemy1.IsAlive);
    // }

    // // sprawdzam zakończenie walki
    // [Fact]
    // public void Finish_ShouldCloseCombat()
    // {
    //     var (_, session) = CreateSession();
    //     var reward = new CombatReward(10, 10, new[] { "sword" });
    //     session.Finish(reward);
    //     Assert.True(session.IsFinished);
    //     Assert.Equal(CombatStateType.End, session.State);
    //     Assert.Equal(reward, session.Reward);
    // }

    // // sprawdzam nie można wykonać pustej akcji
    // [Fact]
    // public void ExecuteSelectedAction_WithoutAction_ShouldFail()
    // {
    //     var (_, session) = CreateSession();

    //     var result = session.ExecuteSelectedAction();

    //     Assert.True(result.IsFailure);
    // }

    // // sprawdzam nie można wykonać attack acion jeśli nie ma targetu
    // [Fact]
    // public void AttackAction_WithoutTarget_ShouldFail()
    // {
    //     var (_, session) = CreateSession();
    //     var action = new AttackAction();
    //     session.SelectAction(action);
    //     var result = session.ExecuteSelectedAction();
    //     Assert.True(result.IsFailure);
    // }

    // // sprawdzam czy target dostaje damage przy akcji ataki
    // [Fact]
    // public void AttackAction_ShouldDealDamage()
    // {
    //     var (participants, session) = CreateSession();
    //     var player = participants.First();
    //     var enemy1 = participants[1];
    //     var enemy2 = participants[2];

    //     session.SetActiveParticipant(player);
    //     session.SetTarget(enemy2);
    //     var action = new AttackAction();
    //     session.SelectAction(action);
    //     var result = session.ExecuteSelectedAction();
    //     Assert.True(result.IsSuccess);
    //     Assert.Equal(enemy1.Stats.MaxHp, enemy1.CurrentHp);
    //     Assert.Equal(enemy2.Stats.MaxHp - player.Stats.Attack, enemy2.CurrentHp);
    // }

    // // testuję UseCase
    // [Fact]
    // public void StartCombtaUseCase_ShouldCreateSession()
    // {
    //     var (participants, _) = CreateSession();
    //     var request = new StartCombatRequest(
    //         participants.First(),
    //         [participants[1], participants[2]]
    //     );
    //     var sut = new StartCombatUseCase();
    //     var result = sut.Execute(request);

    //     Assert.True(result.IsSuccess);
    //     Assert.NotNull(result.Value);
    //     Assert.Equal(3, result.Value.Participants.Count);
    // }
}
