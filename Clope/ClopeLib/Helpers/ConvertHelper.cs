using System.Linq;

namespace ClopeLib.Helpers
{
	public static class ConvertHelper
	{
		public static string ConvertToString<T>(T[] array, string separator)
		{
			const string defaultSeparator = " ";
			const string noElements = "[]";
			const string bracketOpen = "[";
			const string bracketClose = "]";

			if (array == null) return string.Empty;

			if (array.Length == 0) return noElements;

			if (string.IsNullOrEmpty(separator)) separator = defaultSeparator;

			var res = string.Join(
				separator,
				array.Where(x => x != null).Select(x => x.ToString()).ToArray()
			);

			return string.Concat(
				bracketOpen,
				res,
				bracketClose
			);
		}



		public static string[] ToStrings<T>(this T[] array) => array.Select(x => x?.ToString()).ToArray();
	}
}
