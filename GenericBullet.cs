using Godot;
using System;

public partial class GenericBullet : Sprite2D
{
	public Vector2 velocity = new Vector2(0,0);
	public override void _Ready()
	{
		GD.Print("Test");
		base._Ready();
	}

	public override void _Process(double delta)
	{
		Position += velocity;
		base._Process(delta);
	}
}
