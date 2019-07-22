using NUnit.Framework;
using ClopeLib.Helpers;

namespace ClopeLib.UnitTests.Helpers
{
	[TestFixture]
	public class UtilsTests
	{
		[Test]
		public void TrySetValue_DifferentValue_IsTrue()
		{
			const float min = 1.0f;
			const float max = 5.0f;

			var current = 3.0f;
			var newValue = 4.0f;

			var changed = Utils.TrySetValue(ref current, newValue, min, max);

			Assert.AreEqual(
				newValue,
				current
			);

			Assert.IsTrue(changed);
		}



		[TestCase(1.0f, 2.0f)]
		[TestCase(5.0f, 3.0f)]
		[TestCase(1.499f, 1.501f)]
		[TestCase(4.999f, 5.0f)]
		public void TrySetValue_DifferentValues_IsTrue(float current, float newValue)
		{
			const float min = 1.0f;
			const float max = 5.0f;

			var changed = Utils.TrySetValue(ref current, newValue, min, max);

			Assert.AreEqual(
				newValue,
				current
			);
			Assert.IsTrue(changed);
		}



		[TestCase(1.0f, 1.000000001f)]
		[TestCase(5.0f, 4.999999999f)]
		[TestCase(1.499f, 1.499f)]
		[TestCase(4.999f, 4.999f)]
		public void TrySetValue_EqualValues_IsFalse(float current, float newValue)
		{
			const float min = 1.0f;
			const float max = 5.0f;

			var changed = Utils.TrySetValue(ref current, newValue, min, max);

			Assert.IsFalse(changed);
		}
	}
}
