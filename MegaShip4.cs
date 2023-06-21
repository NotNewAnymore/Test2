using Godot;
using System;
using Test2;

public partial class MegaShip4 : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Position += new Vector3(0,0,0.075f);
	}
}
