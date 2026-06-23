// Dungeon != Combat
// Encounter != CombatSession

/// <summary>
/// Zawiera jedno starcie w świecie/dungu. Udostępnia jedynie dane do rozpoczęcia walki.
/// </summary>
public class Encounter
{
    public readonly Guid Id;
    public EncounterState State { get; private set; }
    private IReadOnlyList<Enemy> _enemies;

    public bool IsFinished => State == EncounterState.Completed;
    public bool RewardClaimed => State == EncounterState.RewardClaimed;
    public bool CanEnter => State == EncounterState.Available;
    public IReadOnlyList<Enemy> Enemies => _enemies;

    public Encounter(IReadOnlyList<Enemy> enemies, EncounterState? state = null, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        if (enemies.Count() == 0)
        {
            DebugExtension.Fatal(this, "Encounter requires at least one enemy!");
        }

        _enemies = enemies;
        State = state ?? EncounterState.Available;
        // DebugExtension.Log(this, $"Encounter {Id} created.");
    }

    /// <summary>
    /// Rozpoczyna walkę i tworzy sesję walki.
    /// </summary>
    public void Start()
    {
        State = EncounterState.InProgress;
        DebugExtension.Log(this, $"Encounter {Id} started.");
    }

    /// <summary>
    /// Zablokuj walkę.
    /// </summary>
    /// <remarks>
    /// Ustawia, że nie można "wejść" do tej walki. Np bo to jest walka z bossem.
    /// </remarks>
    public void Lock()
    {
        State = EncounterState.Locked;
        DebugExtension.Log(this, $"Encounter {Id} locked.");
    }

    /// <summary>
    /// Odblokuj walkę.
    /// </summary>
    public void Unlock()
    {
        State = EncounterState.Available;
        DebugExtension.Log(this, $"Encounter {Id} unlocked.");
    }

    /// <summary>
    /// Zakończ walkę i zniszcz sesję.
    /// </summary>
    /// <remarks>
    /// Metoda powinna być wywoływana po ukończeniu walki i odebraniu nagrody.
    /// </remarks>
    public void End()
    {
        if (!RewardClaimed)
        {
            DebugExtension.Fatal(
                this,
                $"The encounter {Id} cannot be finished. The conditions are not met."
            );
        }

        State = EncounterState.Completed;
        DebugExtension.Log(this, $"Encounter {Id} ended.");
    }

    /// <summary>
    /// Oznacza flagi, że nagroda zostła odebrana.
    /// </summary>
    /// <remarks>
    /// Ta metoda musi zostać wykonana przed FinishCombat()
    /// </remarks>
    public void MarkRewardClaimed()
    {
        State = EncounterState.RewardClaimed;
        DebugExtension.Log(this, $"Encounter {Id} has benn marked as rewardClaimed.");
    }
}
