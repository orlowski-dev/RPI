using Godot;

/// <summary>
/// Odpowiada za sterowanie postacią gracza.
/// </summary>
/// <remarks>
/// Wykorzystuje system fizyki do kolizji
/// </remarks>
public partial class PlayerController : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 300;

	private AnimatedSprite2D _sprite;
	
	//[Export]
	//public int RotationSpeed { get; set; } = 10;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	 }

	public override void _PhysicsProcess(double delta)
	{
		MovePlayer(ref delta);
	}

	/// <summary>
	/// Odpowiada za poruszanie się postacią gracza
	/// </summary>
	/// <param name=""></param>
	/// <remarks>
	/// Pobiera kierunek z InputMap
	/// </remarks>
	private void MovePlayer(ref double delta)
	{
		var direction = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");
		// poruszanie
		Velocity = direction * Speed;
		
		if (direction.X < 0)
		{
			_sprite.FlipH = true;
		}
		else if (direction.X > 0)
		{
			_sprite.FlipH = false;
		}
		if (direction == Vector2.Zero)
		{
			_sprite.Play("Idle");
		}
		else
		{
			_sprite.Play("Walking");
		}
		
		//płynna rotacja
		//if (direction != Vector2.Zero)
		//{
			//var targetRotation = direction.Angle() + Mathf.Pi / 2; // domyślnie sprite patrzy w góręę
			//Rotation = Mathf.LerpAngle(Rotation, targetRotation, RotationSpeed * (float)delta);
		//}

		MoveAndSlide();
	}
}
