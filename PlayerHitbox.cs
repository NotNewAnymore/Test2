using Godot;
using System;

namespace Test2 {
	public partial class PlayerHitbox : Sprite2D
	{
		public AudioStreamPlayer deathSound = new AudioStreamPlayer();
		public override void _Ready()
		{
			AddChild(deathSound);
			deathSound.Stream = ResourceLoader.Load<AudioStreamOggVorbis>("res://Hit.ogg");
			base._Ready();
		}
		public override void _Process(double delta)
		{
			DebugData.playerPos = GlobalPosition;
			//GD.Print(DebugData.playerPos);
			base._Process(delta);
			if (DebugData.iFrames > 0)
			{
				DebugData.iFrames--;
			}
			if (DebugData.hit && DebugData.iFrames == 0)
			{
				DebugData.lives -= 1;
				DebugData.iFrames = 60;
				DebugData.hit = false;
				GD.Print($"Hit! Lost a life! Remaining Lives: {DebugData.lives}");
				deathSound.Play(0f);
				
			}
			else if (DebugData.hit && DebugData.iFrames != 0)
			{
				DebugData.hit = false;
				//GD.Print("Hit! Iframes saved you!");
			}
		}
	}
}