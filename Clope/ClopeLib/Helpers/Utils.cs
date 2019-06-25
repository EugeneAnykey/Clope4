using System;

namespace ClopeLib.Helpers
{
	public static class Utils
	{
		public static bool TrySetValue(ref float currentValue, float newValue, float min, float max)
		{
			const float eps = 0.0001f;

			var res = false;

			newValue = newValue < min ? min : max < newValue ? max : newValue;
			if (res = Math.Abs(currentValue - newValue) > eps)
				currentValue = newValue;

			return res;
		}
	}
}
