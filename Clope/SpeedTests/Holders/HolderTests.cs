#define list1
#define struct

using NUnit.Framework;
using System;
using System.Linq;

namespace SpeedTests.Holders.Tests
{
	[TestFixture]
	public class HolderTests
	{
		// factory
		IHolder _GetHolder() =>
#if list
		new HolderList();
#else

	#if struct
			new HolderDic();
	#else
			new HolderDic(true);
	#endif

#endif


		IItem _GetItem(int index, string name) =>
#if struct
		new StructItem(index, name);
#else
		new ClassItem(index, name);
#endif

		readonly string[][] items = new[] {
			new[] { "a", "b", "c" },
			new[] { "a", "d", "a" },
			new[] { "b", "e", "c" },
		};

		void FillDefault(IHolder holder)
		{
			foreach (var line in items)
				holder.PlaceAndGetIndicies(line);
		}

		IItem[] GetItemsForColumn(int col = 0)
		{
			var ss = items.Select(a => a[col]).Distinct().ToArray();
			return ss.Select(s => _GetItem(col, s)).ToArray();
		}



		[Test]
		public void PlaceAndGetIndicies_NullInput_ThrowArgumentNull() => Assert.Catch<ArgumentNullException>(() => _GetHolder().PlaceAndGetIndicies(null));

		[Test]
		public void PlaceAndGetIndicies_EmptyArray_ReturnZeroLengthArray() => Assert.AreEqual(new int[0], _GetHolder().PlaceAndGetIndicies(new string[0]));



		[TestCase("a", "b", "c")]
		[TestCase("a", "b", "a")]
		public void PlaceAndGetIndicies_InputOnce_IsGood(params string[] input)
		{
			var holder = _GetHolder();
			int[] expectedLinks = new[] { 0, 1, 2 };

			var res = holder.PlaceAndGetIndicies(input);

			Assert.AreEqual(
				expectedLinks,
				res
			);
		}



		[TestCase("a", "b", "c", "d")]
		[TestCase("a", "b", "a", "a")]
		public void PlaceAndGetIndicies_InputTwice_LinksAreEqual(params string[] items)
		{
			var holder = _GetHolder();
			int[] expectedLinks = new[] { 0, 1, 2, 3 };

			holder.PlaceAndGetIndicies(items);
			var currentLinks = holder.PlaceAndGetIndicies(items);

			Assert.AreEqual(
				expectedLinks,
				currentLinks
			);
		}



		[Test]
		public void Retrieve_Existed_IsGood()
		{
			const int col = 0;
			IItem[] expected = GetItemsForColumn(col);

			var holder = _GetHolder();
			FillDefault(holder);
			var res = holder.Retrieve(col);

			Assert.AreEqual(expected.Length, res.Length);
			for (int i = 0; i < expected.Length; i++)
				Assert.IsTrue(expected[i].Equals(res[i]));
		}



		[Test]
		public void RetrieveIndicies_NotExisted_EmptyArray()
		{
			var holder = _GetHolder();
			FillDefault(holder);
			const int col = 7;

			Assert.IsTrue(0 == holder.RetrieveIndicies(col).Length);
		}



		[Test]
		public void RetrieveByIndex_Existed_IsGood()
		{
			const int index = 3;
			IItem expected = GetItemsForColumn(1)[1];

			var holder = _GetHolder();
			FillDefault(holder);

			var res = holder.RetrieveByIndex(index);

			Assert.IsTrue(expected.Equals(res));
		}




		[Test]
		public void GetAttributeByLink_NotExisted_IsNull()
		{
			Assert.IsNull(_GetHolder().RetrieveByIndex(3));
		}
	}
}
