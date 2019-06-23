using NUnit.Framework;

using ClopeLib.Helpers;
using System;

namespace ClopeLib.UnitTests.Helpers
{
	[TestFixture]
	public class UtilsTests
	{
		// Equals
		[Test]
		public void SetValue_DifferentValue_IsTrue()
		{
			const float min = 1.0f;
			const float max = 5.0f;

			var current = 3.0f;
			var newValue = 4.0f;

			var changed = Utils.SetValue(ref current, newValue, min, max);

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
		public void Equals_MiscStrings_IsTrue(float current, float newValue)
		{
			const float min = 1.0f;
			const float max = 5.0f;

			var changed = Utils.SetValue(ref current, newValue, min, max);

			Assert.AreEqual(
				newValue,
				current
			);
			Assert.IsTrue(changed);
		}
	}
}
