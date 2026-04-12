using Godot;
using System;

//obsługa zoom kamery

public partial class Camera2d : Camera2D
{
	double _maxZoom = 2;
	double _minZoom = 0.5;
	public override void _UnhandledInput(InputEvent @event)
{
	if (Input.IsActionPressed("mouseWheelUp"))
	{
		if (Zoom.X < _maxZoom) 
		{
			//Zoom.X += 0.1;
			Console.WriteLine("xd");
		}
		
	} else if (Input.IsActionPressed("mouseWheelDown"))
	{
		if (Zoom.X > _minZoom)
		{
			//Zoom.X -= 0.1;
						Console.WriteLine("dx");

		}
	}
}

}
