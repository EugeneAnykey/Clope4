using NUnit.Framework;
using ClopeLib.Data;

namespace ClopeLib.UnitTests.Data
{
	[TestFixture]
	public class AttributeTests
	{
		[TestCase(1, "abc")]
		[TestCase(2, "")]
		[TestCase(-1, null)]
		[TestCase(0, "a")]
		[TestCase(17754, "asfas sfa safs dfs fs ")]
		public void Init_Normal_IsGood(int pos, string name)
		{
			IAttribute at = new Attribute4(pos, name);

			Assert.AreEqual(
				pos,
				at.Position
			);

			Assert.AreEqual(
				name,
				at.Name
			);
		}



		[Test]
		public void Equals_Self_IsTrue()
		{
			IAttribute at = new Attribute4(2, "f");

			Assert.IsTrue(at.Equals(at));
		}

		[TestCase(0, "a", 0, "a")]
		[TestCase(1, "a", 1, "a")]
		[TestCase(1, "z", 1, "z")]
		public void Equals_Similar_IsTrue(int pos1, string name1, int pos2, string name2)
		{
			IAttribute at1 = new Attribute4(pos1, name1);
			IAttribute at2 = new Attribute4(pos2, name2);

			Assert.IsTrue(at1.Equals(at2));
		}



		[TestCase(0, "a", 0, "z")]
		[TestCase(0, "a", 6, "a")]
		[TestCase(0, "a", 6, "z")]
		public void Equals_Different_IsFalse(int pos1, string name1, int pos2, string name2)
		{
			IAttribute at1 = new Attribute4(pos1, name1);
			IAttribute at2 = new Attribute4(pos2, name2);

			Assert.IsFalse(at1.Equals(at2));
		}
	}
}
