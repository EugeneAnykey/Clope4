namespace ClopeLib.Helpers
{
	public static class ArrayHelper
	{
		public static bool Equals<T>(this T[] array1 ,T[] array2)
		{
			if (array1 == array2)
				return true;
			
			if (array1.Length != array2.Length)
				return false;
			
			for (int i = 0; i < array1.Length; i++) {
				if (array1[i] == null || array2[i] == null || !array1[i].Equals(array2[i]))
					return false;
			}

			return true;
		}
	}
}
