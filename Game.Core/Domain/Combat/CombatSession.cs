namespace Game.Core.Domain.Combat;

/// <summary>
/// Reprezentuje pojedynczą sesję walki.
///
/// Odpowiada za: przechowywanie uczestników, zarządzanie aktywną turą, przechowywanie wybranej akcji oraz oznaczanie zakończenia walki. Nie odpowiada za przejścia stanów ani logikę UI.
/// </summary>
public class CombatSession
{
    private readonly List<CombatParticipant> _participants;
    private CombatAction? _selectedAction;

    public CombatStateType State { get; set; }
    public CombatContext Context { get; }

    public IReadOnlyList<CombatParticipant> Participants => _participants;
    public CombatParticipant ActiveParticipant { get; private set; }
    public CombatParticipant NextParticipant { get; private set; }
    public CombatParticipant? Target { get; private set; }

    public int TurnNumber { get; private set; }
    public bool IsFinished { get; private set; }
    public CombatReward? Reward { get; private set; }
    public bool HasSelectedAction => _selectedAction is not null;

    public CombatParticipant Player =>
        _participants.Find((x) => x.Type == CombatParticipantType.Player)
        ?? throw new InvalidOperationException();

    public IReadOnlyList<CombatParticipant> AliveEnemies =>
        _participants.FindAll((x) => x.Type == CombatParticipantType.Enemy && x.IsAlive);

    public CombatSession(IEnumerable<CombatParticipant> participants)
    {
        Context = new CombatContext(this);
        _participants = participants.ToList();
        ActiveParticipant = _participants.First();
        NextParticipant = PeekNextAliveParticipant();
        State = CombatStateType.Start;
    }

    /// <summary>
    /// Ustawia akcję wybraną przez gracza lub AI.
    /// </summary>
    public void SelectAction(CombatAction action)
    {
        _selectedAction = action;
    }

    private CombatAction ConsumeAction()
    {
        if (_selectedAction is null)
        {
            throw new InvalidOperationException();
        }

        var action = _selectedAction;
        _selectedAction = null;

        return action;
    }

    /// <summary>
    /// Wykonuje aktualnie wybraną akcję.
    /// Po wykonaniu akcja jest usuwana z sesji.
    /// </summary>
    /// <returns>
    /// Result określający sukces lub błąd wykonania.
    /// </returns>
    public Result ExecuteSelectedAction()
    {
        // akcja musi być wybrana
        if (!HasSelectedAction)
        {
            return Result.Fail(new("combat.no_action", "No action selected", ErrorType.Validation));
        }

        // pobierz i wyczyść aktualną akcję
        var action = ConsumeAction();

        // delegacja wykonania do konkretnej akcji
        return action.Execute(this);
    }

    /// <summary>
    /// Oznacza zakończenie walki i zapisuje wynik nagrody.
    /// </summary>
    public void Finish(CombatReward reward)
    {
        Reward = reward;
    }

    public void SetTarget(CombatParticipant? target)
    {
        Target = target;
    }

    public void ClearTarget()
    {
        Target = null;
    }

    public void SetActiveParticipant(CombatParticipant participant)
    {
        ActiveParticipant = participant;
    }

    public void UpdateStatus()
    {
        if (!Player.IsAlive || AliveEnemies.Count == 0)
        {
            IsFinished = true;
            State = CombatStateType.End;
        }
    }

    public CombatParticipant PeekNextAliveParticipant()
    {
        var current = _participants.IndexOf(ActiveParticipant);

        for (var i = 1; i <= _participants.Count; i++)
        {
            var next = (current + i) % _participants.Count;

            if (_participants[next].IsAlive)
            {
                return _participants[next];
            }
        }

        throw new InvalidOperationException("No alive participants.");
    }

    public CombatParticipant MoveNextParticipant()
    {
        var next = PeekNextAliveParticipant();

        ActiveParticipant = next;

        NextParticipant = PeekNextAliveParticipant();

        return ActiveParticipant;
    }
}
