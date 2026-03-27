using Godot;
using System;

public partial class GameController : Node
{
	public static GameController Instance { get; private set; }

	private int _health = 100;
	public int Health
	{
		get => _health;
		set
		{
			_health = Mathf.Clamp(value, 0, MaxHealth);
			EmitSignal(SignalName.HealthChanged, _health);
		}
	}

	public int MaxHealth { get; set; } = 100;

	[Signal]
	public delegate void HealthChangedEventHandler(int newHealth);

	public override void _Ready()
	{
		if (Instance != null)
		{
			QueueFree(); // już istnieje – usuń duplikat
			return;
		}
		Instance = this;
	}
}
