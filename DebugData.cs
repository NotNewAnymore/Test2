using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace Test2
{
	static public class DebugData
	{
		static public int numBullets = 0;
		static public Vector2 playerPos= new Vector2(0,0);
		static public int lives = 5;
		static public int iFrames = 0;
		static public bool hit = false;
	}
}
