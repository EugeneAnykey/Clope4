using System.Linq;

namespace ClopeLib.Helpers
{
	public static class ConvertHelper
	{
		public static string[] ToStrings<T>(this T[] array) => array.Select(x => x?.ToString()).ToArray();
	}
}
