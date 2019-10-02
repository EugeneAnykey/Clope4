using System;
using NUnit.Framework;
using ClopeLib.Parsers;

namespace ClopeLib.UnitTests.Parsers
{
	[TestFixture]
	public class ParserTests
	{
		IParser _GetParser() => new Parser();
		IParser _GetParser(char[] splitter) => new Parser() { Splitter = splitter };
		IParser _GetParser(char[] splitter, IElementRule rule) => new Parser(splitter, rule);



		[Test]
		public void Parse_NullInput_ThrowsNullReference()
		{
			const string input = null;
			var parser = _GetParser();
			Assert.Catch<NullReferenceException> (() => parser.Parse(input));
		}
		


		[TestCase("", new string[0])]
		[TestCase("a", new [] {"a"})]
		[TestCase("b,,", new [] {"b"})]
		[TestCase("b,,?", new [] {"b", "?"})]
		[TestCase(",, c,,", new [] {"c"})]
		[TestCase(",d, c,,", new [] {"d", "c"})]
		[TestCase("a, b, g, t, , h", new [] {"a", "b", "g", "t", "h"})]
		public void Parse_NormalInputsWithCommaSpace_ReturnsGood(string input, string[] expected)
		{
			var parser = _GetParser(Parser.CommaSpaceSplitter);
			
			var result = parser.Parse(input);
			
			Assert.AreEqual(
				expected,
				result
			);
		}
		


		[TestCase("b,,?", new [] {"b", null})]
		[TestCase("c, ??", new [] {"c", "??"})]
		[TestCase("b,,?*?", new [] {"b", "?*?"})]
		public void Parse_RuledInputsWithCommaSpace_ReturnsGood(string input, string[] expected)
		{
			var parser = _GetParser(
				Parser.CommaSpaceSplitter,
				new ElementRule(new[] { "?" }, null)
			);
			
			var result = parser.Parse(input);
			
			Assert.AreEqual(
				expected,
				result
			);
		}
	}
}
