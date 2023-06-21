using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
	internal class BulletRemover
	{
		public static void removeTicking(TickingObject bullet)
		{
			Data.tickingObjects.Remove(bullet);
		}
		public static void removeColliding(CollidingObject bullet)
		{
			Data.collidingObjects.Remove(bullet);
		}
	}
}
