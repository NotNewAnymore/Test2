using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
	internal class hBullet
	{
		//Basic variables
		Sprite2D objSprite;
		Behavior behavior;
		float offset;
		int counter = 0;
		bool garbage = false;
		Random random = new Random();
		Vector2 origin;
		int ttl;
		int lastGraze;
		public AudioStreamPlayer graze = new AudioStreamPlayer();

		//Constructor
		public hBullet(Sprite2D objSprite, float offset, Vector2 origin, int ttl, Behavior behavior = Behavior.bDefault)
		{
			this.objSprite = objSprite;
			this.behavior = behavior;
			this.offset = offset;
			this.origin = origin;
			this.ttl = ttl;
			lastGraze = 0;
			Data.numBullets += 1;

			this.objSprite.AddChild(graze);
			graze.Stream = ResourceLoader.Load<AudioStreamOggVorbis>("res://Graze1.ogg");
			graze.VolumeDb -= 10;
		}


		static public Sprite2D GenerateSpriteSquare1(int number) //generates a sprite for the bullet. Could I do this in the constructor? Yes. But I want to add more of these for more sprites later.
		{
			Sprite2D sprite = new Sprite2D();
			sprite.Name = $"Bullet{number}";
			sprite.Texture = ResourceLoader.Load<Texture2D>("res://SquareBullet.png");
			sprite.Hframes = 30;
			sprite.Frame = 0;
			return sprite;
		}
		/// <summary>
		/// Called every frame by the bullet manager. Handles bullet behavior.
		/// </summary>
		/// <exception cref="NotImplementedException"></exception>
		public void Tick()
		{
			counter++;
			if (counter <= objSprite.Hframes-1)
			{
				objSprite.Frame += 1;
			}
			else if (counter >= ttl)
			{
				garbage = true;
			}
			else
			{
				if (Math.Abs(Data.playerPos.DistanceTo(objSprite.GlobalPosition)) <= 8)	//Collision detection. I could not figure out colliders, so here's my solution.
				{
					behavior = Behavior.dead;
				}
			}

			//Graze mechanics
			if (Math.Abs(Data.playerPos.DistanceTo(objSprite.GlobalPosition)) <= 24 && counter >= lastGraze + 15)
			{
				Data.score += 10;
				lastGraze = counter;
				graze.Play();
			}

			


			//Behavior
			switch (behavior)
			{
				case Behavior.dead:
					if (!garbage)
					{
						objSprite.GlobalPosition = new Vector2(-200, -200);
						garbage = true;
						Data.hit = true;
					}
					break;
				case Behavior.bDefault:
					//GD.Print("bDefault");
					break;
				case Behavior.bRadDefault:
					//GD.Print("bRadDefault");
					bRadDefault();
					break;
				case Behavior.aTest1:
					aTest1();
					break;
				case Behavior.bRad1:
					bRad1(origin);
					break;
				case Behavior.bRad2:
					bRad2(origin);
					break;
				case Behavior.vertPattern1:
					vertPattern1();
					break;
				case Behavior.vertPattern2:
					vertPattern2();
					break;
				default:
					throw new NotImplementedException();	//Should never happen.
					//break;
			}
		}

		/// <summary>
		/// Default radial bullet behavior. Go in Offset direction, 1 pixel per frame, from screen center.
		/// </summary>
		public void bRadDefault()
		{
			Vector2 origin = new Vector2(300f, 350f);
			objSprite.GlobalPosition = CoordConv.polarToCart(offset, counter, origin);
		}
		/// <summary>
		/// Makes a thick spiral at the given co-ordinates
		/// </summary>
		/// <param name="origin"></param>
		public void bRad1(Vector2 origin)
		{
			objSprite.GlobalPosition = CoordConv.polarToCart(offset * 2, counter, origin);
		}
		public void bRad2(Vector2 origin)
		{
			objSprite.GlobalPosition = CoordConv.polarToCart(offset * 3 + counter / 4f, ((float)Math.Sin((float)counter / 20) * (20)) + ((float)counter / 2), origin);
		}
		/// <summary>
		/// Test behavior. Used to figure out how the coordinate conversion is broken.
		/// </summary>
		public void aTest1()
		{
			Vector2 origin = new Vector2(300f, 350f);
			objSprite.GlobalPosition = CoordConv.polarToCart(45, counter, origin);
		}
		public void vertPattern1()
		{
			objSprite.GlobalPosition = new Vector2(((offset - ((float)Math.Sin(counter / 50f)) * 10) % 630f) - 10f, counter);
		}
		public void vertPattern2()
		{
			objSprite.GlobalPosition = new Vector2(((offset - ((float)Math.Sin(counter / 50f)) * 10) % 630f) - 10f, 710 - counter);
		}
		//Getters and setters
		public Sprite2D ObjSprite { get => objSprite; set => objSprite = value; }
		public float Offset { get => offset; set => offset = value; }
		public int Counter { get => counter; set => counter = value; }
		public bool Garbage { get => garbage; set => garbage = value; }
		public Behavior Behavior { get => behavior; set => behavior = value; }
		public Vector2 Origin { get => origin; set => origin = value; }
		public int Ttl { get => ttl; set => ttl = value; }
	}
}
