using Game.Core.Application.Results;

namespace Game.Core.Domain.Combat;

/// <summary>
/// Reprezentuje pojedynczą sesję walki.
///
/// Odpowiada za: przechowywanie uczestników, zarządzanie aktywną turą, przechowywanie wybranej akcji oraz oznaczanie zakończenia walki. Nie odpowiada za przejścia stanów ani logikę UI.
/// </summary>
public partial class CombatSession
{
    private readonly List<CombatParticipant> _participants;
    private CombatAction? _selectedAction;

    public CombatStateType State { get; private set; }
    public IReadOnlyList<CombatParticipant> Participants => _participants;
    public CombatParticipant ActiveParticipant { get; private set; }
    public int TurnNumber { get; private set; }
    public bool IsFinished { get; private set; }
    public CombatReward? Reward { get; private set; }
    public bool HasSelectedAction => _selectedAction is not null;

    public CombatSession(IEnumerable<CombatParticipant> participants)
    {
        _participants = participants.ToList();
        ActiveParticipant = _participants.First();
        TurnNumber = 1;
        State = CombatStateType.Start;
    }

    public void BeginPlayerTurn()
    {
        State = CombatStateType.PlayerTurn;
    }

    /// <summary>
    /// Ustawia akcję wybraną przez gracza lub AI.
    /// </summary>
    public void SelectAction(CombatAction action)
    {
        _selectedAction = action;
    }

    public CombatAction ConsumeAction()
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

    public void EndPlayerTurn()
    {
        TurnNumber += 1;
        MoveToNextParticipant();
    }

    /// <summary>
    /// Oznacza zakończenie walki i zapisuje wynik nagrody.
    /// </summary>
    public void Finish(CombatReward reward)
    {
        Reward = reward;
        IsFinished = true;
        State = CombatStateType.End;
    }

    private void MoveToNextParticipant()
    {
        var current = _participants.IndexOf(ActiveParticipant);
        var next = (current + 1) % _participants.Count;
        ActiveParticipant = _participants[next];
    }
}
