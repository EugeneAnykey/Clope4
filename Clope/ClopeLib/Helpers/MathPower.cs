using System;
using System.Collections.Generic;

namespace ClopeLib.Helpers
{
	public class MathPower
	{
		readonly double power;

		readonly List<double> list = new List<double>();



		public double this[int x]
		{
			get {
				while (list.Count <= x)
				{
					list.Add(Math.Pow(list.Count, power));
				}

				return list[x];
			}
		}



		public MathPower(double power)
		{
			this.power = power;
			list.Add(power == 0 ? 1 : 0);
		}
	}
}
