using System;

namespace ClopeLib.Helpers
{
	public static class ArrayHelper
	{
		// Equals
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



		// Contains
		public static bool Contains<T>(this T[] items, T item) where T : IEquatable<T>
		{
			if (items == null || item == null)
				throw new ArgumentNullException();

			for (int i = 0; i < items.Length; i++)
				if (items[i].Equals(item))
					return true;

			return false;
		}



		// Position
		public static int Position<T>(this T[] items, T item) where T : IEquatable<T>
		{
			if (items == null || item == null)
				throw new ArgumentNullException();

			for (int i = 0; i < items.Length; i++)
				if (items[i].Equals(item))
					return i;

			return -1;
		}
	}
}
