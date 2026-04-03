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
        _service = new CombatService(
            enemies:
            [
                new EnemyCharacter(
                    name: "Kuba",
                    maxHp: 300,
                    attack: 30,
                    defense: 10,
                    critChance: 1,
                    level: _gc.PlayerCharacter.Level,
                    enemyType: EnemyType.Normal
                ),
            ]
        );
        _combatSignals.EmitTurnChanged(
            new(playerTurn: _service.PlayerTurn, enemies: _service.Enemies)
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
            new(playerTurn: _service.PlayerTurn, enemies: _service.Enemies)
        );
    }
}
