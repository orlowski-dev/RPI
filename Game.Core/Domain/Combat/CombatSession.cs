/// <summary>
/// Reprezentuje pojedynczą sesję walki.
///
/// Odpowiada za: przechowywanie uczestników, zarządzanie aktywną turą, przechowywanie wybranej akcji oraz oznaczanie zakończenia walki. Nie odpowiada za przejścia stanów ani logikę UI.
/// </summary>
public class CombatSession
{
    private readonly List<Actor> _participants;
    private CombatAction? _selectedAction;
    private CombatAction? _previousAction;

    public CombatAction? PreviousAction => _previousAction;

    public CombatStateType State { get; set; }
    public CombatContext Context { get; }

    public IReadOnlyList<Actor> Participants => _participants;
    public Actor ActiveParticipant { get; private set; }
    public Actor? NextParticipant { get; private set; }
    public Actor? Target { get; private set; }

    public int TurnNumber { get; private set; }
    public bool IsFinished { get; private set; }
    public CombatReward? Reward { get; private set; }
    public bool HasSelectedAction => _selectedAction is not null;
    public bool RewardClaimed { get; private set; } = false;
    private CombatActionResult? _lastActionResult;
    public CombatActionResult? LastActionResult { get; private set; }

    public void SetLastActionResult(CombatActionResult result)
    {
        _lastActionResult = result;
        LastActionResult = result;
    }

    public CombatActionResult? ConsumeLastActionResult()
    {
        if (LastActionResult is null)
            return null;

        LastActionResult = null;
        return _lastActionResult;
    }

    public string StatePlural =>
        State switch
        {
            CombatStateType.Start => "Start",
            CombatStateType.PlayerTurn => "Tura gracza",
            CombatStateType.EnemyTurn => "Tura przeciwnika",
            CombatStateType.PlayerDeath => "Zgon gracza",
            CombatStateType.ResolveTurn => "Przetwarzanie tury",
            CombatStateType.Reward => "Podsumowanie z nagrodą",
            _ => "Koniec",
        };

    public Player Player =>
        _participants.OfType<Player>().FirstOrDefault()
        ?? throw new InvalidOperationException("Player in CombatSession not found!");

    public IReadOnlyList<Enemy> AliveEnemies =>
        _participants.OfType<Enemy>().Where(x => x.IsAlive && x is Enemy).ToList();

    public IReadOnlyList<Enemy> AllEnemies =>
        _participants.OfType<Enemy>().Where(x => x is Enemy).ToList();

    public bool PlayerWon => IsFinished && Player.IsAlive;

    public string GetInfo()
    {
        var temp = new List<string>();
        temp.Add($"State: {StatePlural}");
        temp.Add($"Participants: {DebugExtension.Dump(Participants)}");
        temp.Add($"Active Participant: {ActiveParticipant.Name}");
        temp.Add($"Next Participant: {NextParticipant?.Name}");
        temp.Add($"Target: {Target?.Name}");
        return string.Join('\n', temp);
    }

    public CombatSession(IEnumerable<Actor> participants)
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

    public void ClearSelectedAction()
    {
        _selectedAction = null;
    }

    private CombatAction ConsumeAction()
    {
        if (_selectedAction is null)
        {
            DebugExtension.Fatal(this, "Selected action is null!");
        }

        var action = _selectedAction;
        _previousAction = _selectedAction;
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
    public Result<CombatActionResult> ExecuteSelectedAction()
    {
        // akcja musi być wybrana
        if (!HasSelectedAction)
        {
            return Result<CombatActionResult>.Fail(new("No action selected", ErrorType.Validation));
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
        IsFinished = true;
    }

    public void SetTarget(Actor? target)
    {
        Target = target;
    }

    public void ClearTarget()
    {
        Target = null;
    }

    public void SetActiveParticipant(Actor participant)
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

    private Actor PeekNextAliveParticipant()
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

    public Actor MoveToNextParticipant()
    {
        SetActiveParticipant(NextParticipant ?? throw new InvalidOperationException());

        SetNextParticipant();

        return ActiveParticipant;
    }

    public void SetNextParticipant()
    {
        NextParticipant = PeekNextAliveParticipant();
    }

    public void ClaimReward(CombatReward reward)
    {
        Player.AddExperience(reward.Experience);
        Player.AddGold(reward.Gold);
        RewardClaimed = true;
    }
}
