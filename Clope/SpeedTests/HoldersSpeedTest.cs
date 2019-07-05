using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EugeneAnykey.DebugLib.Loggers;
using SpeedTests.Holders;

namespace SpeedTests
{
	public class HoldersSpeedTest
	{
		public enum ItemsCount
		{
			ten = 1,
			hundred,
			thousand,
			myriad
		}

		// field
		IHolder holderDic = new HolderDic();
		IHolder holderList = new HolderList();



		int itemsMaxCount = 100;
		int iterations;

		readonly ILogger logger;

		List<string[]> lines = new List<string[]>();

		// const
		const double pow = 3.7;// Math.PI;
		const int defaultItemsMaxCount = 100;
		const int mega = 1000 * 1000;



		// init
		public HoldersSpeedTest(ILogger logger)
		{
			this.logger = logger;
		}



		// preparing
		void Clear()
		{
			holderDic = new HolderDic();
			holderList = new HolderList();
		}

		Random r = new Random(DateTime.UtcNow.Millisecond);
		const string latin = "abcdefghijklmnopqrstuvwxyz";
		readonly int latinLen = latin.Length;

		string GenerateString()
		{
			int count = r.Next(0, 3) + 1;
			var cc = new char[count];

			for (int j = 0; j < count; j++)
				cc[j] = latin[r.Next(0, latinLen)];

			return new string(cc);
		}

		void Generate(int itemsMaxCount)
		{
			const int count = 5;

			for (int i = 0; i < itemsMaxCount; i++)
			{
				var ss = new string[count];
				for (int j = 0; j < count; j++)
				{
					ss[j] = GenerateString();
				}
				lines.Add(ss);
			}
		}




		public void Run()
		{
			var a = Enum.GetValues(typeof(ItemsCount)).Cast<ItemsCount>();

			foreach (var count in a)
			{
				itemsMaxCount = (int)Math.Pow(10, (int)count);
				logger.Write($"Items: {itemsMaxCount}.");
				Generate(itemsMaxCount);

				RunAndCompare();
			}
		}



		// private
		void RunAndCompare()
		{
			float[] mils = new float[] { 0.01f, 0.1f };

			for (int i = 0; i < mils.Length; i++)
			{
				Clear();
				float million = mils[i];
				iterations = (int)(million * mega);
				logger.Write($"Times: {million} M.");
				RunTest("Dict Place", TestDicPlace);
				RunTest("List Place", TestListPlace);
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
		void TestDicPlace()
		{
			for (int i = 0; i < iterations; i++)
			{
				var indicies = holderDic.PlaceAndGetIndicies(lines[i % lines.Count]);
			}
		}

		void TestListPlace()
		{
			for (int i = 0; i < iterations; i++)
			{
				var indicies = holderList.PlaceAndGetIndicies(lines[i % lines.Count]);
			}
		}
	}
}
