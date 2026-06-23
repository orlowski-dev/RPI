public class CombatSessionTests
{
    private (Player, Enemy, Enemy) GetActors()
    {
        var tcp = new TestCombatParticipant();
        var player = tcp.Player as Player;
        var enemy1 = tcp.Enemy1 as Enemy;
        var enemy2 = tcp.Enemy2 as Enemy;

        return (player, enemy1, enemy2);
    }

    [Fact]
    [LogTest]
    public void CombatFlow_ShouldBeEnemy1()
    {
        var (player, enemy1, enemy2) = GetActors();
        var start = new StartCombatUseCase();
        var startCombat = new StartCombatUseCase();
        var combatRes = startCombat.Execute(new(player, new[] { enemy1, enemy2 }));
        var startResolveTurn = new ResolveTurnUseCase();

        var session = combatRes.Value.CombatSession;

        session.SetActiveParticipant(player);
        Assert.True(session.ActiveParticipant is Player);
        Assert.True(session.NextParticipant is Enemy);
        Assert.Equal(enemy1.Id, session.NextParticipant.Id);
    }

    [Fact]
    [LogTest]
    public void CombatFlow_ShouldFinishCombat()
    {
        var tcp = new TestCombatParticipant();
        var player = tcp.Player;
        var enemy1 = tcp.Enemy1;
        var enemy2 = tcp.Enemy2;

        var start = new StartCombatUseCase();
        var combat = start.Execute(new(player, new[] { enemy1, enemy2 }));

        var resolve = new ResolveTurnUseCase();
        ResolveCombatResponse response = default!;

        var maxIterations = 10;
        var iterations = 0;
        var end = false;

        while (iterations++ < maxIterations && !end)
        {
            var session = combat.Value.CombatSession;

            if (session.State == CombatStateType.PlayerTurn)
            {
                var target = session.AliveEnemies.FirstOrDefault();
                // Console.WriteLine(DebugExtension.Dump(session.AliveEnemies));
                if (target is null)
                {
                    end = true;
                    break;
                }
                session.SetTarget(target);
                response = resolve
                    .Execute(
                        new(
                            Session: session,
                            StateMachine: combat.Value.StateMachine,
                            Action: new AttackAction()
                        )
                    )
                    .Value;
            }
            else
            {
                response = resolve
                    .Execute(new(Session: session, StateMachine: combat.Value.StateMachine))
                    .Value;
            }

            // Log.Write(
            //     this,
            //     $"{session.PreviousAction?.GetType().Name} | "
            //         + $"{session.ActiveParticipant.Id} - "
            //         + $"{session.Target?.Id ?? "none"} | "
            //         + $"next={session.NextParticipant?.Id}"
            // );
            // Log.Write(this, DebugExtension.Dump(response));

            if (response.CombatFinished)
                break;

            Assert.True(iterations < maxIterations);
        }

        var finish = new FinishCombatUseCase();
        var finishRes = finish.Execute(
            new(Session: combat.Value.CombatSession, StateMachine: combat.Value.StateMachine)
        );

        Assert.True(finishRes.IsSuccess);
        // Log.Write(this, DebugExtension.Dump(finishRes.Value.Dto));

        Assert.Equal(0, player.Exp);
        var claimRewardUseCase = new ClaimCombatRewardUseCase();
        var claimRewardResponse = claimRewardUseCase.Execute(
            new(Session: combat.Value.CombatSession, Reward: finishRes.Value.Reward)
        );
        Assert.True(player.Exp > 0);
        Assert.True(combat.Value.CombatSession.PlayerWon);
    }

    [Fact]
    [LogTest]
    public void CombatFlow_ShouldShouldBeDead()
    {
        var tcp = new TestCombatParticipant();
        var player = tcp.Player;
        var enemy1 = tcp.Enemy1;
        var enemy2 = tcp.Enemy2;

        var start = new StartCombatUseCase();
        var combat = start.Execute(new(player, new[] { enemy1, enemy2 }));

        var resolve = new ResolveTurnUseCase();
        ResolveCombatResponse response = default!;

        var maxIterations = 30;
        var iterations = 0;

        while (iterations++ < maxIterations)
        {
            var session = combat.Value.CombatSession;

            if (session.State == CombatStateType.PlayerTurn)
            {
                var target = session.AliveEnemies.FirstOrDefault();
                if (target is null)
                {
                    break;
                }
                session.SetTarget(target);

                response = resolve
                    .Execute(
                        new(
                            Session: session,
                            StateMachine: combat.Value.StateMachine,
                            Action: new FakeAction()
                        )
                    )
                    .Value;
            }
            else
            {
                response = resolve
                    .Execute(new(Session: session, StateMachine: combat.Value.StateMachine))
                    .Value;
            }

            if (response.CombatFinished)
                break;

            Assert.True(iterations < maxIterations);
            Assert.False(combat.Value.CombatSession.PlayerWon);
        }

        var finish = new FinishCombatUseCase();
        var finishRes = finish.Execute(
            new(Session: combat.Value.CombatSession, StateMachine: combat.Value.StateMachine)
        );

        Assert.True(finishRes.IsSuccess);
        // Log.Write(this, DebugExtension.Dump(finishRes.Value.Dto));
    }
}
