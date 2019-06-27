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
	}
}
