using EugeneAnykey.DebugLib.Loggers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ClopeWin
{
	public class SpeedTest
	{
		const float pow = 2.7f;
		readonly List<double> list = new List<double>();
		readonly Dictionary<int, double> dic = new Dictionary<int, double>();

		readonly ILogger log1;
		readonly Random r = new Random(DateTime.UtcNow.Millisecond);
		const int low = 0;
		const int high = 100;

		const int kilo = 1000;
		const int mega = kilo * kilo;



		// init
		public SpeedTest()
		{
			log1 = new FileLogger("speed_pre.txt");

			for (int i = low; i < high; i++)
			{
				var res = Math.Pow(i, pow);
				list.Add(res);
				dic.Add(i, res);
			}
		}



		public void Run()
		{
			Run(1);
			Run(10);
			Run(100);
		}


		// private
		void Run(int mil)
		{
			log1.Write($"Times: {mil} M, items: {high}.\r\n");
			RunAxe(mil * mega);
			RunLis(mil * mega);
			RunDic(mil * mega);
			log1.Write("\r\n");
		}


		void RunLis(int times)
		{
			Stopwatch w = new Stopwatch();
			w.Start();

			for (int i = 0; i < times; i++)
			{
				//int pos = r.Next(0, 100);
				int pos = i % high;
				var a = 1 + list[pos];
			}

			w.Stop();
			log1.Write($"List> Elapsed: {w.ElapsedMilliseconds} ms.");
		}


		public void RunDic(int times)
		{
			Stopwatch w = new Stopwatch();
			w.Start();

			for (int i = 0; i < times; i++)
			{
				//int pos = r.Next(0, 100);
				int pos = i % high;
				var a = 1 + dic[pos];
			}

			w.Stop();
			log1.Write($"Dic > Elapsed: {w.ElapsedMilliseconds} ms.");
		}


		public void RunAxe(int times)
		{
			Stopwatch w = new Stopwatch();
			w.Start();

			for (int i = 0; i < times; i++)
			{
				//int pos = r.Next(0, 100);
				int pos = i % high;
				var a = 1 + Math.Pow(pos, pow);
			}

			w.Stop();
			log1.Write($"Axe > Elapsed: {w.ElapsedMilliseconds} ms.");
		}
	}
}
