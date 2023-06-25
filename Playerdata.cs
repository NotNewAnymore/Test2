using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
	internal class Playerdata : IComparable<Playerdata>
	{
		uint score;
		string name;

		public Playerdata(uint score, string name)
		{
			this.score = score;
			this.name = name;
		}

		public uint Score { get => score; set => score = value; }
		public string Name { get => name; set => name = value; }

		public int CompareTo(Playerdata other)
		{
			if(score > other.score)
			{
				return -1;
			}
			else if(score < other.score)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}

		public override string ToString()
		{
			return $"{name}:{score}";
		}
	}
}
