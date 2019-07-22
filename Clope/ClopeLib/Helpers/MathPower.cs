using System;
using System.Collections.Generic;

namespace ClopeLib.Helpers
{
	/// <summary>
	/// Math power cacher for single exponent and positive or zero values.
	/// </summary>
	public class MathPower
	{
		readonly double power;

		readonly List<double> list = new List<double>();



		public double this[int x]
		{
			get {
				if (x < 0)
					throw new NegativeValueException();

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
