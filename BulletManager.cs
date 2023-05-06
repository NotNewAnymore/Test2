using Godot;
using System;
using System.Collections.Generic;

namespace Test2
{
	public partial class BulletManager : Node2D
	{
		//delegate void ShootEventHandler(PackedScene bullet, Vector2 direction, Vector2 location);
		//private PackedScene _bullet = GD.Load<PackedScene>("res://generic_bullet.tscn");
		//Sprite2D bulletInstance;	//Bullets used for figuring out how to spawn objects.
		//Sprite2D secondBullet;
		List<hBullet> bullets = new List<hBullet>();
		int counter = 0;
		int lCounter = 0;
		public override void _Ready()
		{
			//Old testing code
			////GD.Print("Test");
			////EmitSignal(SignalName.Shoot, _bullet, Rotation, Position);
			//bulletInstance = GetNode<Sprite2D>("OtherBullet");
			//bulletInstance.Name = "newBullet";
			//GD.Print(bulletInstance.GetScript());
			////AddChild(bulletInstance);
			//bulletInstance.Texture = ResourceLoader.Load<Texture2D>("res://SquareBullet.png"); //I can set the image used by the bullet from here.
			//bulletInstance.Hframes = 30;    //set up the spritesheet
			//bulletInstance.Vframes = 1;
			//bulletInstance.Frame = 0;   //set the frame in the spritesheet. I can replace the animation system with this. Remember, the sheet starts at 0, not 1.
			//bulletInstance.GlobalPosition = new Vector2(10, 10); //I can set the coordinates from here. I can compare cooridnates from here. There is no reason to have the bullets "do" anything at all.
			
			
			base._Ready();
		}
		public override void _Process(double delta)
		{
			//base._Process(delta);
			//bulletInstance.GlobalPosition += new Vector2(0.1f, 0.1f);
			//bulletInstance.Frame += 1;
			//if (bulletInstance.Frame == 29)
			//{
			//	bulletInstance.Frame = 0;
			//}
			//Count frames since game start.
			counter++;
			GD.Print($"Total bullets = {DebugData.numBullets}");
			//Bullet patterns
			if (counter <= 2400)
			{
				BurstPattern1();
			}

			tickBullets();
		}
		public void tickBullets()	//Called every frame, tells the bullets to move. Why don't I use the built-in function for this? Because I might want to add pausing later.
		{
			for (int i = 0; i < bullets.Count; i++)
			{
				bullets[i].Tick();
			}
		}
		public void testPattern1()	//Basic test pattern. Fires bullets every 10 degrees. Deprecated.
		{
			for (int i = 0; i < 360; i += 10)
			{
				bullets.Add(new hBullet(hBullet.GenerateSpriteSquare1(bullets.Count), i,new Vector2(300,300), Behavior.bRadDefault));
				AddChild(bullets[bullets.Count - 1].ObjSprite);
			}
		}
		public void testPattern2()	//Coordinate test pattern. Deprecated.
		{
			if (counter % 1 == 0)
			{
				bullets.Add(new hBullet(hBullet.GenerateSpriteSquare1(bullets.Count), counter,new Vector2(300,300) , Behavior.aTest1));
				AddChild(bullets[bullets.Count - 1].ObjSprite);
			}
		}
		public void BurstPattern1()  //Basic test pattern. Fires bullets every 10 degrees.
		{
			if (counter % 5 == 0)
			{
				for (int i = 0; i < 40; i += 10)
				{
					lCounter += 4;
					generatebullet(hBullet.GenerateSpriteSquare1(bullets.Count), i + lCounter, Behavior.bRad1, new Vector2(300,300));
				}
			}
		}
		/// <summary>
		/// Used to make a bullet. Needs a sprite, offset, behavior, and origin. Also does bullet recycling.
		/// </summary>
		/// <param name="objsprite"></param>
		/// <param name="offset"></param>
		/// <param name="behavior"></param>
		/// <param name="origin"></param>
		public void generatebullet(Sprite2D objsprite, float offset, Behavior behavior, Vector2 origin)
		{
			foreach (hBullet i in bullets)
			{
				if (i.Garbage)
				{
					//i.ObjSprite = objsprite;	//Not a good idea, breaks stuff and even if it did work, would make the whole thing mostly pointless.
					i.ObjSprite.Texture = objsprite.Texture;
					i.Offset = offset;
					i.Behavior = behavior;
					i.Garbage = false;
					i.Origin = origin;
					i.ObjSprite.Frame = 0;
					i.Counter = 0;
					return;
				}
			}
			bullets.Add(new hBullet(objsprite, offset, origin, behavior));
			AddChild(bullets[bullets.Count - 1].ObjSprite);
		}

	}

	public enum Behavior	//Bullet behavior options. Names should corrrespond to bullet behaviors in hBullet.
	{
		bDefault,
		bRadDefault,
		aTest1,
		bRad1
	}

}