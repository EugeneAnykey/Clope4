using System;
using NUnit.Framework;

namespace TestingHelper.UnitTests
{
	[TestFixture]
	public class DelimitedFileTests
	{
		static readonly char[] fieldSep = null;
		static readonly char[] expectedFieldSep = new [] { ',' };
		const string possibleFilename = "1";
		const string recordSep = null;
		const string nullFilename = null;
		const string expectedRecordSep = "\r\n";
		
		[TestCase("1")]
		[TestCase("abc")]
		[TestCase("g.txt")]
		public void Filename_IsGood(string filename)
		{
			var tf = new DelimitedFile(filename, fieldSep, recordSep);
			Assert.AreEqual(
				filename,
				tf.Filename
			);
		}
		
		[Test]
		public void Filename_ThrowsNullReference()
		{
			Assert.Catch<ArgumentNullException>(() => new DelimitedFile(nullFilename, fieldSep, recordSep));
		}
		
		[TestCase(new [] { ' ' })]
		[TestCase(new [] { ',', ' ' })]
		public void ItemSeparators_AreGood(char[] fieldSeparators)
		{
			var tf = new DelimitedFile(possibleFilename, fieldSeparators, recordSep);
			Assert.AreEqual(
				fieldSeparators,
				tf.FieldSeparators
			);
		}
		
		[TestCase(null)]
		[TestCase(new char[0])]
		public void ItemSeparators_AreDefault(char[] fieldSeparators)
		{
			var tf = new DelimitedFile(possibleFilename, fieldSeparators, recordSep);
			Assert.AreEqual(
				expectedFieldSep,
				tf.FieldSeparators
			);
		}
		
		[TestCase("\n")]
		[TestCase("|")]
		public void LineSeparator_IsGood(string recordSeparator)
		{
			var tf = new DelimitedFile(possibleFilename, fieldSep, recordSeparator);
			Assert.AreEqual(
				recordSeparator,
				tf.RecordSeparator
			);
		}
		
		[TestCase(null)]
		[TestCase("")]
		public void LineSeparator_IsDefault(string recordSeparator)
		{
			var tf = new DelimitedFile(possibleFilename, fieldSep, recordSeparator);
			Assert.AreEqual(
				expectedRecordSep,
				tf.RecordSeparator
			);
		}
	}
}
