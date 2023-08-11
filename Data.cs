using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace Test2
{
	static public class Data
	{
		
		static public int numBullets = 0;
		static public Vector2 playerPos= new Vector2(0,0);
		static public int lives = 5;
		static public int iFrames = 0;
		static public bool hit = false;	//Determines if the player is touching a bullet.
		static public uint score = 0;
		static internal List<TickingObject> tickingObjects = new List<TickingObject>();	//Lists all ticking objects controlled by the bullet manager.
		static internal List<CollidingObject> collidingObjects = new List<CollidingObject>();   //Lists all objects that can hit things.
		static internal uint highscore;
		static internal List<uint> scores = new List<uint>();
		static internal string name;
		static public LineEdit nameEntry;
		static internal List<Playerdata> playerdata = new List<Playerdata>();
		static public Sprite2D option = new Sprite2D();
		static public BulletManager bulletManager;
		static public int random;
	}
}
