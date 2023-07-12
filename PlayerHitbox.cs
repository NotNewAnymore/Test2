using Godot;
using System;

namespace Test2 {
	public partial class PlayerHitbox : Sprite2D
	{
		public AudioStreamPlayer deathSound = new AudioStreamPlayer();
		Vector2 shootDir = new Vector2();
		float shootDeg = 0;

		public override void _Ready()
		{
			AddChild(deathSound);
			deathSound.Stream = ResourceLoader.Load<AudioStreamOggVorbis>("res://Hit.ogg");
			this.AddChild(Data.option);
			Data.option.Texture = ResourceLoader.Load<Texture2D>("res://Options Test.png");
			Data.option.Hframes = 90;
			Data.option.Vframes = 4;
			base._Ready();
		}
		public override void _Process(double delta)
		{
			Data.playerPos = GlobalPosition;
			//Data.option.GlobalPosition = Data.playerPos;
			//GD.Print(DebugData.playerPos);
			shootDir = Input.GetVector("Wleft", "Wright", "Wup", "Wdown");  //Input handling for weapons
			shootDeg = (float)Math.Abs((CoordConv.CartToDeg(shootDir)));
			Data.option.Frame = (int)shootDeg;
			//GD.Print($"{shootDeg}");
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