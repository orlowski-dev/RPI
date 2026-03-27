using Godot;
using System;

public partial class TestUIController : Node
{
	private Button increase_health;
	private Button decrease_health;
	private ProgressBar progress_bar;
	

	public override void _Ready()
	{
		increase_health = GetNode<Button>("HBoxContainer/IncreaseHealth");
		decrease_health = GetNode<Button>("HBoxContainer/DecreaseHealth");
		progress_bar = GetNode<ProgressBar>("ProgressBar");

		progress_bar.MaxValue = GameController.Instance.MaxHealth;
		progress_bar.Value = GameController.Instance.Health;
		
		GameController.Instance.HealthChanged += OnHealthChanged;
		
		increase_health.Pressed += IncreaseHealthPressed;
		decrease_health.Pressed += DecreaseHealthPressed;
	}

	public override void _ExitTree()
	{
		// Odsubskrybuj sygnał przy usunięciu węzła
		GameController.Instance.HealthChanged -= OnHealthChanged;
	}

	private void OnHealthChanged(int newHealth)
	{
		progress_bar.Value = newHealth;
	}


	
		private void IncreaseHealthPressed()
		{
			progress_bar.Value += 10;
		}

		private void DecreaseHealthPressed()
		{
			progress_bar.Value -= 10;	
		}
	}
