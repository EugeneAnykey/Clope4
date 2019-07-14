#define retest

using System;
using System.Collections.Generic;
using System.Linq;
using ClopeLib.Data;
using NUnit.Framework;

namespace ClopeLib.UnitTests.Data
{
	[TestFixture]
	public class TransactionHashTest
	{
		#region prepare
		// lines count to test = GenerateRepeatTimes * MaxIntegersInLine * (Duplicate ? 2 : 1);
		const int GenerateRepeatTimes = 150;
		const int MaxIntegersInLine = 20;
		const bool Duplicate = true;



		int[] Generate(int count, Random r) => new int[count].Select(a => r.Next(100)).ToArray();



		List<int[]> GenerateListOfInts()
		{
			var r = new Random();
			var result = new List<int[]>();

			for (int times = 0; times < GenerateRepeatTimes; times++)
				for (int i = 0; i < MaxIntegersInLine; i++)
					result.Add(Generate(i, r));

			// +100% duplicates:
			if (Duplicate)
				result.AddRange(result);

			return result;
		}



		List<int> GetTestLinks(out int equalHashes)
		{
			var hashes = new List<int>();
			equalHashes = 0;

			foreach (var item in GenerateListOfInts())
			{
				var t = new Transaction(item);
				var hash = t.GetHashCode();
				if (hashes.Contains(hash))
					equalHashes++;

				hashes.Add(hash);
			}

			return hashes;
		}
		#endregion



		[Test]
		public void GetHashCode_MiscInput_IsGood()
		{
			var hashes = GetTestLinks(out int equalsCount);

			var equalsPercent = (float)equalsCount / hashes.Count;
			const float EqualsPercentThreshold = 0.1f;

			Assert.LessOrEqual(
				equalsPercent,
				EqualsPercentThreshold
			);
		}
	}
}
