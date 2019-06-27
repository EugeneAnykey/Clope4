using NUnit.Framework;
using ClopeLib.Data;
using System;

namespace ClopeLib.UnitTests.Data
{
	[TestFixture]
	public class AttributeStoreAtListTests
	{
		// factory
		IAttributeStore _GetAttributeStore() => new AttributeStoreAtList();

		void FillDefault(IAttributeStore store)
		{
			store.PlaceAndGetLinks("a", "b", "c");  // 1, 2, 3
			store.PlaceAndGetLinks("a", "d", "a");  // 1, 4, 5
			store.PlaceAndGetLinks("b", "e", "c");  // 6, 7, 3
		}



		[Test]
		public void PlaceAndGetLinks_NullInput_ThrowArgumentNull() => Assert.Catch<ArgumentNullException>(() => _GetAttributeStore().PlaceAndGetLinks(null));

		[Test]
		public void PlaceAndGetLinks_EmptyArray_ThrowEmptyArray() => Assert.Catch<EmptyArrayException>(() => _GetAttributeStore().PlaceAndGetLinks(new string[0]));



		[TestCase("a", "b", "c")]
		[TestCase("a", "b", "a")]
		public void PlaceAndGetLinks_InputOnce_IsGood(params string[] input)
		{
			IAttributeStore store = _GetAttributeStore();
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
			IAttributeStore store = _GetAttributeStore();
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
			IAttributeStore store = _GetAttributeStore();
			FillDefault(store);
			const int col = 0;

			IAttribute[] expected = new[] { new Attribute4(col, "a"), new Attribute4(col, "b") };

			var res = store.GetAttributes(col);

			Assert.IsTrue(expected[0].Equals(res[0]));
			Assert.IsTrue(expected[1].Equals(res[1]));
		}



		[Test]
		public void GetAttributes_NotExisted_EmptyArray()
		{
			IAttributeStore store = _GetAttributeStore();
			FillDefault(store);
			const int col = 7;

			Assert.IsTrue(0 == store.GetAttributes(col).Length);
		}



		[Test]
		public void GetAttributeByLink_Existed_IsGood()
		{
			IAttributeStore store = _GetAttributeStore();
			FillDefault(store);

			IAttribute expected = new Attribute4(1, "d");

			var res = store.GetAttributeByLink(4);

			Assert.IsTrue(expected.Equals(res));
		}



		[Test]
		public void GetAttributeByLink_NotExisted_IsNull()
		{
			Assert.IsNull(_GetAttributeStore().GetAttributeByLink(3));
		}
	}
}
