using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
	internal class BaseBullet : TickingObject, CollidingObject
	{
		//Basic variables
		Sprite2D objSprite;
		protected float offset;
		protected int counter = 0;
		bool garbage = false;
		Random random = new Random();
		protected Vector2 origin;
		int ttl;
		int lastGraze;
		public AudioStreamPlayer graze = new AudioStreamPlayer();
		static int bulletCount = 0;

		public Sprite2D ObjSprite { get => objSprite; set => objSprite = value; }

		//Constructor
		public BaseBullet(float offset, Vector2 origin, int ttl)
		{
			bulletCount += 1;
			this.offset = offset;
			this.origin = origin;
			this.ttl = ttl;
			lastGraze = 0;
			Data.numBullets += 1;
			objSprite = new Sprite2D();

			objSprite.AddChild(graze);
			graze.Stream = ResourceLoader.Load<AudioStreamOggVorbis>("res://Graze1.ogg");
			graze.VolumeDb -= 10;

			objSprite.Name = $"Bullet{bulletCount}";
			objSprite.Texture = ResourceLoader.Load<Texture2D>("res://SquareBullet.png");
			objSprite.Hframes = 30;
			objSprite.Frame = 0;
			objSprite.GlobalPosition = origin;
			
			Data.tickingObjects.Add(this);
			Data.collidingObjects.Add(this);
		}

		//Destructor
		~BaseBullet()
		{
			objSprite.Free();
			objSprite = null;
			//GD.Print($"Removed bullet");
		}

		public void Tick(double delta)
		{
			counter++;

			Animate();	//Bullet animations.
			Behavior();	//Bullet behavior.
			Dispose();      //Determines if a bullet should be disposed of.
			Strike();   //Detects if the bullet is hitting the player. If it is, sets Data.hit to true and sets garbage to true.
			Graze();	//Grazing behavior


		}
		public void Dispose()
		{
			if (counter >= ttl || garbage==true)
			{                                                   //Determine if bullet is garbage. If it is, put it offscreen, set garbage to true, and then return.
				counter = ttl + 5;
				garbage = true;
				objSprite.Position = new Vector2(-1000, 1000);
				//GD.Print($"Bullet disposed");
				return;
			}
		}
		public void Animate()
		{
			if (counter <= objSprite.Hframes - 1)      //Bullet fade-in animation.
			{
				objSprite.Frame += 1;
			}
		}
		public virtual void Behavior()
		{
			objSprite.GlobalPosition = origin;  //Stationary behavior
		}

		public virtual void Strike()
		{
			if (Math.Abs(Data.playerPos.DistanceTo(objSprite.GlobalPosition)) <= 8) //Collision detection. I could not figure out colliders, so here's my solution.
			{
				objSprite.GlobalPosition = new Vector2(-200, -200);
				garbage = true;
				Data.hit = true;
			}
		}

		public virtual void Graze()
		{
			if (Math.Abs(Data.playerPos.DistanceTo(objSprite.GlobalPosition)) <= 24 && counter >= lastGraze + 15)
			{
				Data.score += 10;
				lastGraze = counter;
				graze.Play();
			}
		}

		public bool Garbage()
		{
			
			return garbage;
		}
	}
}
