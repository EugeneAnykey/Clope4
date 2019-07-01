using System;
using NUnit.Framework;
using ClopeLib.Parsers;

namespace ClopeLib.UnitTests.Parcers
{
	[TestFixture]
	public class ParcerTests
	{
		[Test]
		public void Parce_NullInput_ThrowsNullReference()
		{
			const string input = null;
			var parcer = new Parser();
			Assert.Catch<NullReferenceException> (() => parcer.Parse(input));
		}
		


		[TestCase("", new string[0])]
		[TestCase("a", new [] {"a"})]
		[TestCase("b,,", new [] {"b"})]
		[TestCase("b,,?", new [] {"b", "?"})]
		[TestCase(",, c,,", new [] {"c"})]
		[TestCase(",d, c,,", new [] {"d", "c"})]
		[TestCase("a, b, g, t, , h", new [] {"a", "b", "g", "t", "h"})]
		public void Parce_NormalInputsWithCommaSpace_ReturnsGood(string input, string[] expected)
		{
			var parcer = new Parser();
			parcer.Splitter = Parser.CommaSpaceSplitter;
			
			var result = parcer.Parse(input);
			
			Assert.AreEqual(
				expected,
				result
			);
		}
		


		[TestCase("b,,?", new [] {"b", null})]
		[TestCase("c, ??", new [] {"c", "??"})]
		[TestCase("b,,?*?", new [] {"b", "?*?"})]
		public void Parce_RuledInputsWithCommaSpace_ReturnsGood(string input, string[] expected)
		{
			var parcer = new Parser(
				Parser.CommaSpaceSplitter,
				new ElementRule(new[] { "?" }, null)
			);
			
			var result = parcer.Parse(input);
			
			Assert.AreEqual(
				expected,
				result
			);
		}
	}
}