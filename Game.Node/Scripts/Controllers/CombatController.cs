using Godot;

/// <summary>
/// Kontroler zarządzający przebiegiem walki.
/// </summary>
public partial class CombatController : Node
{
    private CombatService _service;
    private Signals _signals => Signals.Instance;
    private CombatSignals _combatSignals => CombatSignals.Instance;
    private GameManager _gm => GameManager.Instance;
    private Logger _logger => Logger.Instance;
    private string _scriptName;

    public override void _Ready()
    {
        _scriptName = "(Prototype)" + this.GetType().Name;

        _combatSignals.SkipTurn += OnSkipTurnAction;
        _combatSignals.AttackAction += OnAttackAction;
        _combatSignals.DefenseAction += OnDefenseAction;

        var pch = _gm.PlayerCharacter;
        _service = new CombatService(
            playerCharacter: pch,
            enemy: new(
                name: "Wróg publiczny",
                maxHp: 300,
                attack: 30,
                defense: 10,
                critChance: 1,
                level: pch.Level,
                enemyType: EnemyType.Normal
            )
        );

        _combatSignals.EmitDataSender(GetData());
    }

    public override void _ExitTree()
    {
        _combatSignals.SkipTurn -= OnSkipTurnAction;
        _combatSignals.AttackAction -= OnAttackAction;
        _combatSignals.DefenseAction -= OnDefenseAction;
    }

    /// <summary>
    /// Kończy turę i wysyła dane.
    /// </summary>
    private void OnTurnEnded()
    {
        _service.ChangeTurn();
        _combatSignals.EmitDataSender(GetData());
    }

    /// <summary>
    /// Tworzy obiekt danych walki.
    /// </summary>
    private CombatData GetData()
    {
        return new(
            state: _service.State,
            playerCharacter: _service.PlayerCharacter,
            enemy: _service.Enemy
        );
    }

    /// <summary>
    /// Wysyła dane walki do UI.
    /// </summary>
    private void SendData()
    {
        _combatSignals.EmitDataSender(GetData());
    }

    /// <summary>
    /// Obsługuje atak gracza.
    /// </summary>
    private void OnAttackAction()
    {
        var damageTaken = _service.Attack(_service.PlayerCharacter, _service.Enemy);
        _logger.Write(
            LogLevel.Info,
            _scriptName,
            $"Gracz zadaje {damageTaken} obrażeń przeciwnikowi."
        );

        if (CheckIfCombatEnded())
        {
            _combatSignals.EmitDataSender(GetData());
            return;
        }

        OnTurnEnded();
        DoEnemyMove();
    }

    /// <summary>
    /// Wykonuje ruch przeciwnika.
    /// </summary>
    private async void DoEnemyMove(bool defenseAction = false)
    {
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        var damageTaken = _service.Attack(_service.Enemy, _service.PlayerCharacter, defenseAction);
        _logger.Write(
            LogLevel.Info,
            _scriptName,
            $"Przeciwnik zadaje {damageTaken} obrażeń graczowi."
        );

        if (CheckIfCombatEnded())
        {
            _combatSignals.EmitDataSender(GetData());
            return;
        }

        OnTurnEnded();
    }

    /// <summary>
    /// Sprawdza zakończenie walki.
    /// </summary>
    private bool CheckIfCombatEnded()
    {
        if (_service.CombatEnded)
        {
            // emit (_service.CombatState)
            return true;
        }
        // wyświetl UI - UIController
        return false;
    }

    /// <summary>
    /// Obsługa obrony gracza.
    /// </summary>
    private void OnDefenseAction()
    {
        OnTurnEnded();
        DoEnemyMove(defenseAction: true);
    }

    /// <summary>
    /// Obsługa pominięcia tury.
    /// </summary>
    private void OnSkipTurnAction()
    {
        OnTurnEnded();
        DoEnemyMove();
    }
}
