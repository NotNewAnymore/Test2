using Godot;
using System;
using System.Collections.Generic;
using static Godot.Projection;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Test2
{
	public partial class BulletManager : Node2D
	{
		List<hBullet> bullets = new List<hBullet>();
		int counter = 0;
		int lCounter = 0;
		int zBuffer = 1;
		bool saved = false;
		

		public override void _Ready()
		{
			//Old testing code
			
			//counter = 4500;

			if (Godot.FileAccess.FileExists("test.save"))	//Open save if it exists
			{
				using var saveGame = Godot.FileAccess.Open("test.save", Godot.FileAccess.ModeFlags.Read);	//Load save into memory as a string
				GD.Print("File exists!");
				//Data.highscore = saveGame.Get32();
				uint dataIn = 1;    //Stores input data. 
				string stringIn = "";
				int datcounter = 0;     //Makes sure the whole file is loaded, even if there is an all zero byte in it somewhere. Should not be neccesary, but it was not working without this.

				while (dataIn != 0 && datcounter <= 5)
				{
					dataIn = saveGame.Get32();  //Load a byte from SaveGame.
					if (dataIn != 0)    //If the byte is not zero, add it to the scoreboard.
					{
						for (int i = 0; i < 3; i++)
						{
							stringIn += (char)saveGame.Get16();
						}
						Playerdata pDIn = new Playerdata(dataIn, stringIn);
						Data.playerdata.Add(pDIn);
						stringIn = "";
						GD.Print(pDIn);
						//Data.scores.Add(dataIn);
						//GD.Print(dataIn);
						//datcounter = 0;
					}
					else     //If the byte is zero, print Null Score to the console and increase datcounter by 1.
					{
						GD.Print("Null score");
						datcounter += 1;
					}
				}

				Data.playerdata.Sort();
				Data.highscore = Data.playerdata[0].Score;

					//while (dataIn != 0 && datcounter <=5)
					//{
					//	dataIn = saveGame.Get32();	//Load a byte from SaveGame.
					//	if (dataIn != 0)	//If the byte is not zero, add it to the scoreboard.
					//	{
					//		Data.scores.Add(dataIn);
					//		GD.Print(dataIn);
					//		datcounter = 0;
					//	}
					//	else     //If the byte is zero, print Null Score to the console and increase datcounter by 1.
					//	{
					//		GD.Print("Null score");
					//		datcounter += 1;
					//	}

					//}
					//Data.scores.Sort();		//Sort the scores the wrong way
					//Data.scores.Reverse();	//Fix the score sorting
					//Data.highscore = Data.scores[0];	//Set the highscore to the largest score.


				}

			base._Ready();
		}
		public override void _Process(double delta)
		{
			if (zBuffer >= 2048)	//Reset zBuffer when it gets too big
			{
				zBuffer = 1;
			}
			//if (counter % 60 == 0)
			//{
			//	GD.Print($"Current score is {Data.score}");
			//}
			//Count frames since game start.
			counter++;
			//GD.Print($"Total bullets = {DebugData.numBullets}");		//Prints bullet count!
			//GD.Print($"Total TickingObjects = {Data.tickingObjects.Count}");
			//GD.Print(delta);
			//Bullet patterns
			if (Data.score > Data.highscore)
			{
				Data.highscore = Data.score;
			}
			if (counter <= 200)
			{
				testPattern1();
			}
			else if (counter <= 1000)
			{
				BurstPattern1();
			}
			else if (counter <= 1200)
			{
				BurstPattern2();
			}
			else if (counter <= 2200)
			{
				BurstPattern1();
			}
			else if (counter <= 2500)
			{

			}
			else if (counter <= 4500)
			{
				lCounter += 1;
				VertPattern1();
			}
			else if (counter <= 5000)
			{
				lCounter = 0;
			}
			else if (counter <= 7000)       //Tentacle and hail of bullets
			{
				lCounter += 1;
				BurstPattern3();
				VertPattern2();
			}
			else if (counter <= 8000)       //See above, but without the hail
			{
				BurstPattern3();
			}
			else if (counter <= 10000)      //Wavy tentacle of bullets
			{
				lCounter += (int)(Math.Sin(counter / 40f) * 3);
				BurstPattern3();
			}
			if (!saved && counter >= 10000 || Data.lives <= 0 && !saved)	//Save game
			{
				counter = 200000;   //Stop bullet spawning.
				if (Data.nameEntry == null)		//Get player's name.
				{
					Data.nameEntry = new LineEdit();
					this.AddChild(Data.nameEntry);
					Data.nameEntry.GlobalPosition = new Vector2(300, 300);
					Data.nameEntry.Name = "name entry";
					Data.nameEntry.Text = "";
					Data.nameEntry.MaxLength = 3;
					GD.Print($"Position of nameEntry is:{Data.nameEntry.GlobalPosition}.");
				}
				if (Data.nameEntry.Text.Length == Data.nameEntry.MaxLength)		//if the player has filled out their name, save the game.
				{
					//Data.scores.Add(Data.score);
					Data.playerdata.Add(new Playerdata(Data.score,Data.nameEntry.Text));
					GD.Print(Data.playerdata[0]);
					using var saveGame = Godot.FileAccess.Open("test.save", Godot.FileAccess.ModeFlags.Write);  //Set up saveGame to write to test.save

					foreach (Playerdata item in Data.playerdata)
					{
						saveGame.Store32(item.Score);
						for (int i = 0; i < 3; i++)
						{
							saveGame.Store16(item.Name[i]);
						}
					}

					
					//saveGame.Store32((uint)Data.highscore);		//Save a score. Saved as a 32-bit unsigned integer at the start of test.save.
					//foreach (uint item in Data.scores) //Save all scores.
					//{
					//	if (item != 0)
					//	{
					//		saveGame.Store32(item);     //Scores are saved as 32 bit bytes. Godot didn't like CSV helper, and it's built in save features are mostly for saving gamestates, so I had to do this.
					//	}

					//}
					GD.Print("Saved game");
					saved = true;
				}

				
			}

			tickBullets();
			ticktickingObjects(delta);
		}



		public void defaultBullet(float offset, int ttl, Vector2 origin)
		{
			BaseBullet bullet = new BaseBullet(offset, origin, ttl);
			bullet.ObjSprite.ZIndex = zBuffer;
			zBuffer++;
			AddChild(bullet.ObjSprite);
		}

		public void ticktickingObjects(double delta)   //Called every frame, tells the bullets to move. Why don't I use the built-in function for this? Because I might want to add pausing later.
		{


			for (int i = 0; i < Data.tickingObjects.Count; i++)
			{
				if (Data.tickingObjects[i].Garbage())
				{
					Data.tickingObjects.RemoveAt(i);
				}
			}
			for (int i = 0; i < Data.collidingObjects.Count; i++)
			{
				if (Data.collidingObjects[i].Garbage())
				{
					Data.collidingObjects.RemoveAt(i);
				}
			}

			for (int i = 0; i < Data.tickingObjects.Count; i++)
			{
				Data.tickingObjects[i].Tick(delta);
			}
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
			if (counter % 20 == 0)
			{
				defaultBullet(0, 500, new Vector2(300, 300));
				//bullets.Add(new hBullet(hBullet.GenerateSpriteSquare1(bullets.Count), i,new Vector2(300,300), 1000, Behavior.bRadDefault));
				//AddChild(bullets[bullets.Count - 1].ObjSprite);
			}
		}
		public void testPattern2()	//Coordinate test pattern. Deprecated.
		{
			if (counter % 1 == 0)
			{
				bullets.Add(new hBullet(hBullet.GenerateSpriteSquare1(bullets.Count), counter,new Vector2(300,300), 100, Behavior.aTest1));
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
					generatebullet(hBullet.GenerateSpriteSquare1(bullets.Count), i + lCounter, 900, Behavior.bRad1, new Vector2(300,300));
				}
			}
		}
		/// <summary>
		/// Basic test pattern. Fires bullets every 10 degrees. Also has extremely hard to dodge sine wave bullets.
		/// </summary>
		public void BurstPattern2()  
		{
			if (counter % 5 == 0)
			{
				for (int i = 0; i < 10; i += 10)
				{
					lCounter += 4;
					generatebullet(hBullet.GenerateSpriteSquare1(bullets.Count), i + lCounter, 1000, Behavior.bRad2, new Vector2(300, 300));
				}
				for (int i = 0; i < 30; i += 10)
				{
					lCounter += 1;
					generatebullet(hBullet.GenerateSpriteSquare1(bullets.Count), i + lCounter * 2, 900, Behavior.bRad1, new Vector2(300, 300));
				}
			}
		}
		public void BurstPattern3()  //Basic test pattern. Fires bullets every 10 degrees.
		{
			if (counter % 10 == 0)
			{
				for (int i = 0; i <= 150; i += 30)
				{
					generatebullet(hBullet.GenerateSpriteSquare1(bullets.Count), i + (lCounter / 8f), 900, Behavior.bRad2, new Vector2(300, 300));
				}
			}
		}
		/// <summary>
		/// Vertically oriented bullet pattern with wavy bullets.
		/// </summary>
		public void VertPattern1()
		{
			if (counter%30 == 0)
			{
				for (int i = 0; i < 10; i += 2)
				{
					generatebullet(hBullet.GenerateSpriteSquare1(bullets.Count), (lCounter * 1.5f - i) * 40, 900, Behavior.vertPattern1, new Vector2(300, 300));
					generatebullet(hBullet.GenerateSpriteSquare1(bullets.Count), (lCounter * 1f - i) * 40, 900, Behavior.vertPattern2, new Vector2(300, 300));
				}
			}
		}
		/// <summary>
		/// Vertically oriented bullet pattern with wavy bullets.
		/// </summary>
		public void VertPattern2()
		{
			if (counter % 30 == 0)
			{
				for (int i = 0; i < 10; i += 2)
				{

					generatebullet(hBullet.GenerateSpriteSquare1(bullets.Count), (lCounter * 0.5f - i) * 40, 900, Behavior.vertPattern2, new Vector2(300, 300));
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
		public void generatebullet(Sprite2D objsprite, float offset, int ttl, Behavior behavior, Vector2 origin)
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
					i.Ttl = ttl;
					i.ObjSprite.ZIndex = zBuffer;
					zBuffer += 1;
					return;
				}
			}
			bullets.Add(new hBullet(objsprite, offset, origin, ttl, behavior));
			bullets[bullets.Count - 1].ObjSprite.ZIndex = zBuffer;
			zBuffer += 1;
			AddChild(bullets[bullets.Count - 1].ObjSprite);
		}

	}

	public enum Behavior	//Bullet behavior options. Names should corrrespond to bullet behaviors in hBullet.
	{
		dead,
		bDefault,
		bRadDefault,
		aTest1,
		bRad1,
		bRad2,
		vertPattern1,
		vertPattern2
	}
}