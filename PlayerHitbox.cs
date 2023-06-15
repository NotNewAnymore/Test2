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
			Data.playerPos = GlobalPosition;
			//GD.Print(DebugData.playerPos);
			base._Process(delta);
			if (Data.iFrames > 0)
			{
				Data.iFrames--;
			}
			if (Data.hit && Data.iFrames == 0)
			{
				Data.lives -= 1;
				Data.iFrames = 60;
				Data.hit = false;
				GD.Print($"Hit! Lost a life! Remaining Lives: {Data.lives}");
				deathSound.Play(0f);
				
			}
			else if (Data.hit && Data.iFrames != 0)
			{
				Data.hit = false;
				//GD.Print("Hit! Iframes saved you!");
			}
		}
	}
}