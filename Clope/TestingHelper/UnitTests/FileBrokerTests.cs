using System;
using NUnit.Framework;

namespace TestingHelper.UnitTests
{
	[TestFixture]
	public class FileBrokerTests
	{
		// const
		readonly char[] fieldSep = new [] { ',' };
		const string recordSep = "\r\n";



		[TestCase(@"", "", "")]
		[TestCase(@"onlyFile.name", "", "onlyFile.name")]
		[TestCase(@"OnlyDirName", "OnlyDirName", "")]
		[TestCase(@"2/1", "2", "1")]
		[TestCase(@"../some/path/abc.txt", "../some/path", "abc.txt")]
		public void TestPath_MiscInput_IsGood(string expected, string dirPath, string filename)
		{
			DelimitedFile file = new DelimitedFile(filename, fieldSep, recordSep);

			var fileBroker = new FileBroker(dirPath);

			var res = fileBroker.GetFilePath(file);

			Assert.AreEqual(
				expected,
				res.Replace("\\", "/")
			);
		}



		[Test]
		public void Init_NullInput_ThrowsArgumentNull()
		{
			Assert.Catch<ArgumentNullException>(() => new FileBroker(null));
		}

		[Test]
		public void GetFilePath_NullInput_ThrowsArgumentNull()
		{
			Assert.Catch<ArgumentNullException>(() => new FileBroker("").GetFilePath(null));
		}
	}
}
