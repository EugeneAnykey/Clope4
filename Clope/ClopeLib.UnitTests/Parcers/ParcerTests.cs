using System;
using NUnit.Framework;
using ClopeLib.Parcers;

namespace ClopeLib.UnitTests.Parcers
{
	[TestFixture]
	public class ParcerTests
	{
		[Test]
		public void Parce_NullInput_ThrowsNullReference()
		{
			const string input = null;
			var parcer = new Parcer();
			Assert.Catch<NullReferenceException> (() => parcer.Parce(input));
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
			var parcer = new Parcer();
			parcer.Splitter = Parcer.CommaSpaceSplitter;
			
			var result = parcer.Parce(input);
			
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
			var parcer = new Parcer(
				Parcer.CommaSpaceSplitter,
				new ElementRule(new[] { "?" }, null)
			);
			
			var result = parcer.Parce(input);
			
			Assert.AreEqual(
				expected,
				result
			);
		}
	}
}