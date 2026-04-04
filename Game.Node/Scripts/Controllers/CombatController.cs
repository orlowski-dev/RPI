using System;
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
        _combatSignals.TurnEnded += OnTurnEnded;
        _combatSignals.AttackAction += OnAttackAction;
        _combatSignals.DefenseAction += OnDefenseAction;

        _service = new CombatService(
            playerCharacter: _gc.PlayerCharacter,
            enemy: new(
                name: "Kuba",
                maxHp: 300,
                attack: 30,
                defense: 10,
                critChance: 1,
                level: _gc.PlayerCharacter.Level,
                enemyType: EnemyType.Normal
            )
        );
        _combatSignals.EmitTurnChanged(
            new(
                turn: _service.Turn,
                playerCharacter: _service.PlayerCharacter,
                enemy: _service.Enemy
            )
        );
    }

    public override void _ExitTree()
    {
        _combatSignals.TurnEnded -= OnTurnEnded;
        _combatSignals.AttackAction -= OnAttackAction;
        _combatSignals.DefenseAction -= OnDefenseAction;
    }

    private void OnTurnEnded()
    {
        _service.ChangeTurn();
        _combatSignals.EmitTurnChanged(
            new(
                turn: _service.Turn,
                playerCharacter: _service.PlayerCharacter,
                enemy: _service.Enemy
            )
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
        OnTurnEnded();
    }

    private void OnDefenseAction()
    {
        OnTurnEnded();
        DoEnemyMove(defenseAction: true);
    }
}
