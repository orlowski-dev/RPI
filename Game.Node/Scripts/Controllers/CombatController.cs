using Godot;

public partial class CombatController : Node
{
    private CombatService _service;
    private Signals _signals => Signals.Instance;
    private CombatSignals _combatSignals => CombatSignals.Instance;
    private GameController _gc => GameController.Instance;
    private string _scriptName;

    public override void _Ready()
    {
        _scriptName = "(Prototype)" + this.GetType().Name;
        _combatSignals.SkipTurn += OnSkipTurnAction;
        _combatSignals.AttackAction += OnAttackAction;
        _combatSignals.DefenseAction += OnDefenseAction;

        _service = new CombatService(
            playerCharacter: _gc.PlayerCharacter,
            enemy: new(
                name: "Wróg publiczny",
                maxHp: 300,
                attack: 30,
                defense: 10,
                critChance: 1,
                level: _gc.PlayerCharacter.Level,
                enemyType: EnemyType.Normal
            )
        );
        _combatSignals.EmitTurnChanged(GetData());
    }

    public override void _ExitTree()
    {
        _combatSignals.SkipTurn -= OnSkipTurnAction;
        _combatSignals.AttackAction -= OnAttackAction;
        _combatSignals.DefenseAction -= OnDefenseAction;
    }

    private void OnTurnEnded()
    {
        _service.ChangeTurn();
        _combatSignals.EmitTurnChanged(GetData());
    }

    private CombatData GetData()
    {
        return new(
            state: _service.State,
            playerCharacter: _service.PlayerCharacter,
            enemy: _service.Enemy
        );
    }

    private void OnAttackAction()
    {
        var damageTaken = _service.Attack(_service.PlayerCharacter, _service.Enemy);
        Logger.Write(
            LogLevel.Info,
            _scriptName,
            $"Gracz zadaje {damageTaken} obrażeń przeciwnikowi."
        );

        if (CheckIfCombatEnded())
        {
            return;
        }

        OnTurnEnded();
        DoEnemyMove();
    }

    private async void DoEnemyMove(bool defenseAction = false)
    {
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        var damageTaken = _service.Attack(_service.Enemy, _service.PlayerCharacter, defenseAction);
        Logger.Write(
            LogLevel.Info,
            _scriptName,
            $"Przeciwnik zadaje {damageTaken} obrażeń graczowi."
        );

        if (CheckIfCombatEnded())
        {
            return;
        }

        OnTurnEnded();
    }

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

    private void OnDefenseAction()
    {
        OnTurnEnded();
        DoEnemyMove(defenseAction: true);
    }

    private void OnSkipTurnAction()
    {
        OnTurnEnded();
        DoEnemyMove();
    }
}
