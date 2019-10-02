using NUnit.Framework;
using ClopeLib.Data;
using System;
using System.Linq;

namespace ClopeLib.UnitTests.Data
{
	[TestFixture]
	public class AttributeStoreTests
	{
		// factory
		IAttributeStore<string> _GetAttributeStore() => new AttributeStore<string>();

		readonly string[][] attributes = new[] {
			new[] { "a", "b", "c" },
			new[] { "a", "d", "a" },
			new[] { "b", "e", "c" },
		};

		void FillStoreDefault(IAttributeStore<string> store)
		{
			foreach (var line in attributes)
				store.PlaceAndGetLinks(line);
		}

		string[] GetAttributesForColumn(int col = 0)
		{
			//return attributes.Select(a => )
			return attributes.Select(a => a[col]).Distinct().ToArray();
			//return ss.Select(s => new StringAttribute(col, s)).ToArray();
		}



		[Test]
		public void PlaceAndGetLinks_NullInput_ThrowArgumentNull() => Assert.Catch<ArgumentNullException>(() => _GetAttributeStore().PlaceAndGetLinks(null));

		[Test]
		public void PlaceAndGetLinks_EmptyArray_ThrowEmptyArray() => Assert.Catch<EmptyArrayException>(() => _GetAttributeStore().PlaceAndGetLinks(new string[0]));

		[Test]
		public void GetAttributes_NotExisted_ThrowArgumentOutOfRange()
		{
			var store = _GetAttributeStore();
			FillStoreDefault(store);
			const int col = 7;

			Assert.Catch<ArgumentOutOfRangeException>(() => store.GetAttributes(col));
		}



		[TestCase("a", "b", "c")]
		[TestCase("a", "b", "a")]
		public void PlaceAndGetLinks_InputOnce_IsGood(params string[] input)
		{
			var store = _GetAttributeStore();
			int[] expectedLinks = new[] { 1, 2, 3 };

			var res = store.PlaceAndGetLinks(input);

			Assert.AreEqual(
				expectedLinks,
				res
			);
		}



		[TestCase("a", "b", "c", "d")]
		[TestCase("a", "b", "a", "a")]
		public void PlaceAndGetLinks_InputTwice_LinksAreEqual(params string[] items)
		{
			var store = _GetAttributeStore();
			int[] expectedLinks = new[] { 1, 2, 3, 4 };

			store.PlaceAndGetLinks(items);
			var currentLinks = store.PlaceAndGetLinks(items);

			Assert.AreEqual(
				expectedLinks,
				currentLinks
			);
		}



		[Test]
		public void GetAttributes_Existed_IsGood()
		{
			const int col = 0;
			var expected = GetAttributesForColumn(col);

			var store = _GetAttributeStore();
			FillStoreDefault(store);
			var res = store.GetAttributes(col);

			Assert.AreEqual(expected.Length, res.Length);
			for (int i = 0; i < expected.Length; i++)
				Assert.IsTrue(expected[i].Equals(res[i]));
		}



		[Test]
		public void GetAttributeByLink_Existed_IsGood()
		{
			const int link = 4;
			var expected = GetAttributesForColumn(1)[1];
			
			var store = _GetAttributeStore();
			FillStoreDefault(store);

			var res = store.GetAttributeByLink(link);

			Assert.IsTrue(expected.Equals(res));
		}



		[Test]
		public void GetAttributeByLink_NotExisted_IsNull()
		{
			Assert.IsNull(_GetAttributeStore().GetAttributeByLink(3));
		}
	}
}
