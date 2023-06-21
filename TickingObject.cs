using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
	internal interface TickingObject
	{
		public void Tick(double delta);
		public bool Garbage();
	}
}
