public class CombatSessionTests
{
    private (List<Actor>, CombatSession) CreateSession()
    {
        var tcp = new TestCombatParticipant();
        var player = tcp.Player;
        var enemy1 = tcp.Enemy1;
        var enemy2 = tcp.Enemy2;

        var session = new CombatSession(new List<Actor>() { player, enemy1, enemy2 });

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
                Console.WriteLine(DebugExtension.Dump(session.AliveEnemies));
                if (target is null)
                {
                    end = true;
                    break;
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
            }
            else
            {
                dto = resolve
                    .Execute(new(Session: session, StateMachine: combat.Value.StateMachine))
                    .Value;
            }

            Console.WriteLine(
                $"{session.ActiveParticipant.Id} atakuje {session.Target?.Id ?? null}. Next participant is: {session.NextParticipant?.Id}"
            );

            Console.WriteLine(DebugExtension.Dump(dto));

            if (dto.CombatFinished)
                break;

            Assert.True(iterations < maxIterations);
        }

        var finish = new FinishCombatUseCase();
        var finishRes = finish.Execute(
            new(Session: combat.Value.CombatSession, StateMachine: combat.Value.StateMachine)
        );
    }
}
