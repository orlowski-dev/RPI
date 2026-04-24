using Godot;
using Game.Core.Data;
using Game.Core.Services;

/// <summary>
/// Kontroler zarządzający przebiegiem walki.
/// </summary>
public partial class CombatController : Node
{
	private CombatService _service;
	private Signals Signals => Signals.Instance;
	private CombatSignals CombatSignals => CombatSignals.Instance;
	private GameManager GameManager => GameManager.Instance;
	private Logger Logger => Logger.Instance;
	private string _scriptName;

	public override void _Ready()
	{
		_scriptName = "(Prototype)" + this.GetType().Name;

		CombatSignals.SkipTurn += OnSkipTurnAction;
		CombatSignals.AttackAction += OnAttackAction;
		CombatSignals.DefenseAction += OnDefenseAction;

		var rewardDb = new EnemyRewardsDB();
		var rewardService = new RewardService(rewardDb);

		_service = new CombatService(
			playerCharacter: GameManager.PlayerCharacter,
			enemy: GameManager.EnemyCharacter,
			rewardService: rewardService
		);

		CombatSignals.EmitDataSender(GetData());
	}

	public override void _ExitTree()
	{
		CombatSignals.SkipTurn -= OnSkipTurnAction;
		CombatSignals.AttackAction -= OnAttackAction;
		CombatSignals.DefenseAction -= OnDefenseAction;
	}

	/// <summary>
	/// Kończy turę i wysyła dane.
	/// </summary>
	private void OnTurnEnded()
	{
		_service.ChangeTurn();
		CombatSignals.EmitDataSender(GetData());
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
		CombatSignals.EmitDataSender(GetData());
	}

	/// <summary>
	/// Obsługuje atak gracza.
	/// </summary>
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
			CombatSignals.EmitDataSender(GetData());
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
		Logger.Write(
			LogLevel.Info,
			_scriptName,
			$"Przeciwnik zadaje {damageTaken} obrażeń graczowi."
		);

		if (CheckIfCombatEnded())
		{
			CombatSignals.EmitDataSender(GetData());
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

			if (_service.State == CombatState.PlayerWon)
			{
				_service.PlayerCharacter.AddExp(25);
				Signals.EmitGameStateChanged(new GameManagerData(GameState.Dungeon));
			}
			else
			{
				_service.PlayerCharacter.Heal(9999);
				Signals.EmitGameStateChanged(new GameManagerData(GameState.City));
			}

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
