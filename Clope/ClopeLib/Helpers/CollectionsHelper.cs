using System.Collections.Generic;

namespace ClopeLib.Helpers
{
	public static class CollectionsHelper
	{
		public static void Enqueue<T>(this Queue<T> queue, T[] items)
		{
			foreach (var item in items)
			{
				queue.Enqueue(item);
			}
		}



		public static void Enqueue<T>(this Queue<T> queue, IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				queue.Enqueue(item);
			}
		}
	}
}
