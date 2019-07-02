using System;
using NUnit.Framework;
using ClopeLib.Readers;
using System.IO;
using System.Collections.Generic;

namespace ClopeLib.UnitTests.Readers
{
	[TestFixture]
	public class ReaderTests
	{
		// factory
		Reader _GetReader(string filename) => new Reader(filename);



		#region exceptions tests
		[Test]
		public void Init_NotExistingFile_ThrowsFileNotFound()
		{
			Assert.Catch<FileNotFoundException>(() => _GetReader(FileDeployer.not_existing_file));
		}

		[Test]
		public void Init_NullString_ThrowsArgumentNull()
		{
			Assert.Catch<ArgumentNullException>(() => _GetReader(null as string));
		}

		[Test]
		public void Init_NullStream_ThrowsArgumentNull()
		{
			Assert.Catch<ArgumentNullException>(() => new Reader(null as Stream));
		}
		#endregion



		#region file tests
		[Test]
		public void Init_Normal_Good()
		{
			const int magicNum = 15;
			var data = FileDeployer.data1;
			var fname = FileDeployer.GetFilename();

			FileDeployer.DeployFile(fname, data);

			using (var reader = _GetReader(fname))
			{
				reader.LinesToReadAtOnce = magicNum;

				Assert.AreEqual(
					magicNum,
					reader.LinesToReadAtOnce
				);

				Assert.AreEqual(
					false,
					reader.ReachedEndOfFile
				);
			}

			FileDeployer.UnDeployFile(fname);
		}



		[Test]
		public void GetData_Normal_Good()
		{
			var expected = FileDeployer.data1;
			var fname = FileDeployer.GetFilename();

			FileDeployer.DeployFile(fname, expected);

			using (var reader = _GetReader(fname))
			{
				var result = reader.GetData(expected.Length);

				Assert.AreEqual(
					expected,
					result
				);
			}

			FileDeployer.UnDeployFile(fname);
		}

		[Test]
		public void GetData_NegativeCount_ReturnEmpty()
		{
			var expected = new string[0];
			var data = FileDeployer.data1;
			var fname = FileDeployer.GetFilename();

			FileDeployer.DeployFile(fname, data);

			using (var reader = _GetReader(fname))
			{
				var result = reader.GetData(-1);

				Assert.AreEqual(
					expected,
					result
				);
			}

			FileDeployer.UnDeployFile(fname);
		}

		[Test]
		public void GetData_TwoTimes_Good()
		{
			var expected = FileDeployer.data1;
			var fname = FileDeployer.GetFilename();

			FileDeployer.DeployFile(fname, expected);

			using (var reader = _GetReader(fname))
			{
				reader.GetData(expected.Length - 1);
				var result = reader.GetData(1) as List<string>;

				Assert.AreEqual(
					expected[expected.Length - 1],
					result[0]
				);
			}

			FileDeployer.UnDeployFile(fname);
		}

		[Test]
		public void GetData_AskForMoreLinesThanExists_Good()
		{
			var expected = FileDeployer.data1;
			var fname = FileDeployer.GetFilename();

			FileDeployer.DeployFile(fname, expected);

			using (var reader = _GetReader(fname))
			{
				var result = reader.GetData(expected.Length + 1);

				Assert.AreEqual(
					expected,
					result
				);
			}

			FileDeployer.UnDeployFile(fname);
		}
		#endregion
	}
}
