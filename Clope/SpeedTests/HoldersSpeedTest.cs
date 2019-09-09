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

		// const
		const int mega = 1_000_000;



		// field
		readonly ILogger logger;

		IHolder holderDic = new HolderDic();
		IHolder holderList = new HolderList();

		int itemsMaxCount = 100;
		int iterations;

		List<string[]> lines = new List<string[]>();

		Random r = new Random(DateTime.UtcNow.Millisecond);



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



		string GenerateString()
		{
			const string latin = "abcdefghijklmnopqrstuvwxyz";

			int len = r.Next(0, 3) + 1;
			var cc = new char[len];

			for (int j = 0; j < len; j++)
				cc[j] = latin[r.Next(0, latin.Length)];

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
			var countNames = Enum.GetValues(typeof(ItemsCount)).Cast<ItemsCount>();

			foreach (var countName in countNames)
			{
				itemsMaxCount = (int)Math.Pow(10, (int)countName);
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
				iterations = (int)(mils[i] * mega);
				logger.Write($"Times: {mils[i]} M.");
				RunTestWithStopwatch("Dict Place", TestDicPlace);
				RunTestWithStopwatch("List Place", TestListPlace);
				logger.Write("");
			}
		}

		void RunTestWithStopwatch(string name, Action action)
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
