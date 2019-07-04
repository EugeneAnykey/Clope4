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
		IAttributeStore _GetAttributeStore() => new AttributeStoreAtDic();

		readonly string[][] attributes = new[] {
			new[] { "a", "b", "c" },
			new[] { "a", "d", "a" },
			new[] { "b", "e", "c" },
		};

		void FillStoreDefault(IAttributeStore store)
		{
			foreach (var line in attributes)
				store.PlaceAndGetLinks(line);
		}

		Attribute4[] GetAttributesForColumn(int col = 0)
		{
			var ss = attributes.Select(a => a[col]).Distinct().ToArray();
			return ss.Select(s => new Attribute4(col, s)).ToArray();
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
			int[] expectedLinks = new[] { 0, 1, 2 };

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
			int[] expectedLinks = new[] { 0, 1, 2, 3 };

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
			Attribute4[] expected = GetAttributesForColumn(col);

			IAttributeStore store = _GetAttributeStore();
			FillStoreDefault(store);
			var res = store.GetAttributes(col);

			Assert.AreEqual(expected.Length, res.Length);
			for (int i = 0; i < expected.Length; i++)
				Assert.IsTrue(expected[i].Equals(res[i]));
		}



		[Test]
		public void GetAttributes_NotExisted_EmptyArray()
		{
			IAttributeStore store = _GetAttributeStore();
			FillStoreDefault(store);
			const int col = 7;

			Assert.IsTrue(0 == store.GetAttributes(col).Length);
		}



		[Test]
		public void GetAttributeByLink_Existed_IsGood()
		{
			const int link = 3;
			IAttribute expected = GetAttributesForColumn(1)[1];
			
			IAttributeStore store = _GetAttributeStore();
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
