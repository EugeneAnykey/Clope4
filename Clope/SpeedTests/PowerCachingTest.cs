using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EugeneAnykey.DebugLib.Loggers;

namespace SpeedTests
{
	public class PowerCachingTest
	{
		public enum ItemsCount
		{
			ten = 1,
			hundred,
			thousand,
			myriad
		}

		// field
		readonly List<double> list = new List<double>();
		readonly LinkedList<double> linked = new LinkedList<double>();
		readonly Dictionary<int, double> dic = new Dictionary<int, double>();
		int itemsMaxCount = 100;
		int[] randomLikeIndices;
		int iterations;

		readonly ILogger logger;

		// const
		const double pow = 3.7;// Math.PI;
		const int defaultItemsMaxCount = 100;
		const int mega = 1000 * 1000;



		// init
		public PowerCachingTest(ILogger logger)
		{
			this.logger = logger;
		}



		// preparing
		void Clear()
		{
			list.Clear();
			linked.Clear();
			dic.Clear();
		}

		void Generate(int itemsMaxCount)
		{
			Clear();
			for (int i = 0; i < itemsMaxCount; i++)
			{
				var res = Math.Pow(i, pow);
				list.Add(res);
				linked.AddLast(res);
				dic.Add(i, res);
				randomLikeIndices[i] = i;
			}
			Shuffle(randomLikeIndices);
		}

		static void Shuffle(int[] arr)
		{
			var r = new Random(DateTime.UtcNow.Millisecond);
			int len = arr.Length;
			for (int i = 0; i < arr.Length / 2; i++)
			{
				int k = r.Next(0, len);
				if (i == k)
					continue;

				// swap
				var t = arr[i];
				arr[i] = arr[k];
				arr[k] = t;
			}
		}



		public void Run()
		{
			var a = Enum.GetValues(typeof(ItemsCount)).Cast<ItemsCount>();

			foreach (var count in a)
			{
				itemsMaxCount = (int)Math.Pow(10, (int)count);
				randomLikeIndices = new int[itemsMaxCount];
				logger.Write($"Items: {itemsMaxCount}.");
				Generate(itemsMaxCount);

				RunAndCompare();
			}
		}



		// private
		void RunAndCompare()
		{
			int[] mils = new int[] { 1, 10, 100 };

			for (int i = 0; i < mils.Length; i++)
			{
				int million = mils[i];
				iterations = million * mega;
				logger.Write($"Times: {million} M.");
				RunTest("Direct", TestDirect);
				RunTest("List  ", TestList);
				RunTest("Linked", TestLinked);
				RunTest("Dict  ", TestDict);
				logger.Write("");
			}
		}

		void RunTest(string name, Action action)
		{
			Stopwatch w = new Stopwatch();
			w.Start();

			action();

			w.Stop();
			logger.Write($"{name}> Elapsed: {w.ElapsedMilliseconds} ms.");
		}



		// Actions
		void TestDirect()
		{
			for (int i = 0; i < iterations; i++)
			{
				int pos1 = randomLikeIndices[i % itemsMaxCount];
				var a = 1 + Math.Pow(pos1, pow);
			}
		}

		void TestList()
		{
			for (int i = 0; i < iterations; i++)
			{
				int pos1 = randomLikeIndices[i % itemsMaxCount];
				var a = 1 + list[pos1];
			}
		}

		void TestLinked()
		{
			for (int i = 0; i < iterations; i++)
			{
				int pos1 = randomLikeIndices[i % itemsMaxCount];

				foreach (var item in linked)
				{
					if (pos1-- > 0)
						continue;
					var a = 1 + item;
					break;
				}
			}
		}

		void TestDict()
		{
			for (int i = 0; i < iterations; i++)
			{
				int pos1 = randomLikeIndices[i % itemsMaxCount];
				var a = 1 + dic[pos1];
			}
		}
	}
}
