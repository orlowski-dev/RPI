using System;
using Godot;

public partial class CombatController : Node
{
    private CombatService _service;
    private Signals _signals => Signals.Instance;
    private CombatSignals _combatSignals => CombatSignals.Instance;
    private GameController _gc => GameController.Instance;

    public override void _Ready()
    {
        _combatSignals.TurnEnded += OnTurnEnded;
        _combatSignals.AttackAction += OnAttackAction;
        // _service = new CombatService(
        //     enemies:
        //     [
        //         new EnemyCharacter(
        //             name: "Kuba",
        //             maxHp: 300,
        //             attack: 30,
        //             defense: 10,
        //             critChance: 1,
        //             level: _gc.PlayerCharacter.Level,
        //             enemyType: EnemyType.Normal
        //         ),
        //     ]
        // );
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
        _service.Enemy.TakeDamage(10);
        OnTurnEnded();
    }
}
