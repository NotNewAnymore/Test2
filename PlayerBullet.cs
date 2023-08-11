using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
	internal class PlayerBullet : BaseBullet
	{
		public PlayerBullet(float offset, Vector2 origin, int ttl) : base(offset, origin, ttl)
		{
	
		}

		public override void Behavior()
		{
			//offset = offset + (float)Math.Sin(counter);
			if (counter == 1)
			{
				offset = offset + (Data.random / 64f);
				GD.Print($"Random is {Data.random}, offset is {offset}");
			}

			ObjSprite.GlobalPosition = CoordConv.polarToCart(offset, counter * 7 + 70, origin) ;
		}
		public override void Strike()
		{
			
		}
	}
}
