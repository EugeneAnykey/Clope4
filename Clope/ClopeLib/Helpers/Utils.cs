using System;

namespace ClopeLib.Helpers
{
	public static class Utils
	{
		public static bool SetValue(ref float currentValue, float newValue, float min, float max)
		{
			const float eps = 0.000001f;

			var res = false;

			newValue = newValue < min ? min : max < newValue ? max : newValue;
			res = Math.Abs(currentValue - newValue) < eps;
			currentValue = newValue;

			return res;
		}
	}
}
