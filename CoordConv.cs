using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace Test2
{
	/// <summary>
	/// Holds methods to convert between coordinate systems.
	/// </summary>
	static internal class CoordConv
	{
		/// <summary>
		/// Convert polar to cartesian coordinates. r is angle, t is distance.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="t"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		static internal Vector2 polarToCart(float r, float t ,Vector2 offset)
		{
			r = r / (float)(180/Math.PI); //I do not understand why this fixes the co-ordinates. But trial and error shows me that it does. Mostly, I assume. I only need it to be accurate to 1 pixel @ 360 degrees, it probably drifts after that.
			//Ok I asked Zot. The function was written for Radians. The website said it was for degrees... Zot says the Sin and Cos functions in C# appear to be set up for radians.
			float x;
			float y;
			//Convert between polar and cartesian coordinates
			//A^2 + B^2 = C^2. I forgot trig. Time to keep look it up.
			x = t * (float)Math.Cos(r);
			y = t * (float)Math.Sin(r);
			//GD.Print(r);

			Vector2 result = new Vector2(x, y);
			result += offset;
			return result;
		}
	}
}
