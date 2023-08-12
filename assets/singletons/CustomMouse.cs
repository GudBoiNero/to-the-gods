using Godot;
using System;

public partial class CustomMouse : Sprite2D
{
	public override void _Ready()
	{
		// Input.SetCustomMouseCursor(DefaultCursor);
	}

	public override void _Process(double delta)
	{
		// Input.SetCustomMouseCursor(...);
	}
}
