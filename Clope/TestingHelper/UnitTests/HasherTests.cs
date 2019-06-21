#define NeedStudy_No       // output data to file (for study purpose):

using NUnit.Framework;

namespace TestingHelper.UnitTests
{
	[TestFixture]
	public class HasherTests
	{
		[Test]
		public void HasherTest_Alphabet_Good()
		{
			const int ids = 20;
			const bool doubled = true;

			HasherTestItem[] table = HasherDataPreparer.GetTableWith_LatinAlphabet(doubled, ids); // 20 * double latin ~ 13.5k

#if NeedStudy
			const string filename = "HasherTest_Alphabet.txt";
			new Outputer(filename).Output(table);
#endif

			const int expected = 0;
			var res = HasherCounter.CountEqualsTableElems(table);

			Assert.AreEqual(
				expected,
				res
			);
		}



		[Test]
		public void HasherTest_FourEqualPairs_Good()
		{
			HasherTestItem[] table = HasherDataPreparer.GetTableWith_FourEqualPairs();

#if NeedStudy
			const string filename = "HasherTest_FourEqualPairs.txt";
			new Outputer(filename).Output(table);
#endif

			const int expected = 4;
			var res = HasherCounter.CountEqualsTableElems(table);

			Assert.AreEqual(
				expected,
				res
			);
		}



		[Test]
		public void HasherTest_ContainsEmpties_Good()
		{
			HasherTestItem[] table = HasherDataPreparer.GetTableWith_ContainsEmpties();

#if NeedStudy
			const string filename = "HasherTest_ContainsEmpties.txt";
			new Outputer(filename).Output(table);
#endif

			const int expected = 0;
			var res = HasherCounter.CountEqualsTableElems(table);

			Assert.AreEqual(
				expected,
				res
			);
		}



		[Test]
		public void HasherTest_Letters_Good()
		{
			// prepare data:
			HasherTestItem[] table = HasherDataPreparer.GetTableWith_Letters();

#if NeedStudy
			const string filename = "HasherTest_Letters.txt";
			new Outputer(filename).Output(table);
#endif

			const int expected = 0;
			var res = HasherCounter.CountEqualsTableElems(table);

			Assert.AreEqual(
				expected,
				res
			);
		}



		[Test]
		public void HasherTest_Words_Good()
		{
			// prepare data:
			HasherTestItem[] table = HasherDataPreparer.GetTableWith_Words();

#if NeedStudy
			const string filename = "HasherTest_Words.txt";
			new Outputer(filename).Output(table);
#endif

			const int expected = 0;
			var res = HasherCounter.CountEqualsTableElems(table);

			Assert.AreEqual(
				expected,
				res
			);
		}
	}
}
