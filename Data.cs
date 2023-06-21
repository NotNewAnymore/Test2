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
		static public int score = 0;
		static internal List<TickingObject> tickingObjects = new List<TickingObject>();	//Lists all ticking objects controlled by the bullet manager.
		static internal List<CollidingObject> collidingObjects = new List<CollidingObject>();   //Lists all objects that can hit things.
		static internal int highscore;
	}
}
