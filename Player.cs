using Godot;
using System;

public partial class Player : Node2D
{
	float speed = 10;
	Vector2 velocity;
	Vector2 inputDir;
	public override void _Ready()
	{

	}
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("Focus"))
		{
			speed = 3;
		}
		else
		{
			speed = 8;
		}
		inputDir = Input.GetVector("left", "right", "up", "down");
		velocity = inputDir * speed;
		this.Position += velocity;

		if (this.Position.X > 600)
		{
			this.Position = new Vector2(600, this.Position.Y);
		}
		if (this.Position.X < 0)
		{
			this.Position = new Vector2(0, this.Position.Y);
		}
		if (this.Position.Y > 700)
		{
			this.Position = new Vector2(this.Position.X, 700);
		}
		if (this.Position.Y < 0)
		{
			this.Position = new Vector2(this.Position.X, 0);
		}
	}
}
