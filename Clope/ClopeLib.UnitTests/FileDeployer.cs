using System.IO;

namespace ClopeLib.UnitTests
{
	public static class FileDeployer
	{
		public const string not_existing_file = "not_existing.file";
		const string filemask = "test{0}.txt";

		public static readonly string[] data1 = new[] {
			"abc",
			"second line",
			"something more. Maybe even more.",
			"Latest line."
			};

		public static string GetFilename()
		{
			int i = 0;
			string res;

			do
			{
				res = string.Format(filemask, i++);
			} while (File.Exists(res));

			return res;
		}

		public static void DeployFile(string filename, string[] contents)
		{
			File.WriteAllLines(filename, contents);
		}

		public static void UnDeployFile(string filename)
		{
			if (File.Exists(filename))
				File.Delete(filename);
		}
	}
}
