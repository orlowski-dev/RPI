public class CombatSessionTests
{
    private (CombatParticipant, CombatParticipant, CombatSession) CreateSession()
    {
        var player = TestCombatParticipant.Player;
        var enemy = TestCombatParticipant.Enemy;

        var session = new CombatSession(new[] { player, enemy });

        return (player, enemy, session);
    }

    // sprawdzam czy CombatSession ustawia stan początkowy i wybiera pierwszego uczestnika
    [Fact]
    public void Constructor_ShouldInitializeSession()
    {
        var (player, enemy, session) = CreateSession();

        Assert.Equal(CombatStateType.Start, session.State);
        Assert.Equal(player, session.ActiveParticipant);
        Assert.False(session.IsFinished);
        Assert.Equal(1, session.TurnNumber);
    }

    // sprawdzam UI wykonuje akcje a CombatSession ją posiada
    [Fact]
    public void SelectAction_ShouldMarkActionAsSelected()
    {
        var (_, _, session) = CreateSession();

        var action = new FakeAction();

        session.SelectAction(action);
        Assert.True(session.HasSelectedAction);
    }

    // sprawdzam czy akcja została wykonana
    [Fact]
    public void ExecuteSelectedAction_ShouldConsumeAction()
    {
        var (_, _, session) = CreateSession();
        var action = new FakeAction();
        session.SelectAction(action);
        var result = session.ExecuteSelectedAction();
        Assert.True(result.IsSuccess);
        Assert.False(session.HasSelectedAction);
        Assert.True(action.Executed);
    }

    // sprawdza kolejność tur
    [Fact]
    public void EndPlayerTurn_ShouldMoveToNextParticipant()
    {
        var (player, enemy, session) = CreateSession();
        session.EndPlayerTurn();
        Assert.Equal(enemy, session.ActiveParticipant);
        Assert.Equal(2, session.TurnNumber);
    }

    // sprawdzam zakończenie walki
    [Fact]
    public void Finish_ShouldCloseCombat()
    {
        var (_, _, session) = CreateSession();
        var reward = new CombatReward(10, 10, new[] { "sword" });
        session.Finish(reward);
        Assert.True(session.IsFinished);
        Assert.Equal(CombatStateType.End, session.State);
        Assert.Equal(reward, session.Reward);
    }

    // sprawdzam nie można wykonać pustej akcji
    //
    [Fact]
    public void ExecuteSelectedAction_WithoutAction_ShouldFail()
    {
        var (_, _, session) = CreateSession();

        var result = session.ExecuteSelectedAction();

        Assert.True(result.IsFailure);
    }
}
