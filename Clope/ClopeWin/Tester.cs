#define mult1

using System;
using System.Collections.Generic;
using System.Diagnostics;
using ClopeLib;
using ClopeLib.Algo;
using ClopeLib.Data;
using ClopeLib.Parsers;
using ClopeLib.Previews;
using ClopeLib.Readers;
using EugeneAnykey.DebugLib.Loggers;

namespace ClopeWin
{
	public class Tester
	{
		// predefs
		readonly string[] determineAsNulls = new[] { "?", string.Empty };



		// field
		List<string> input;
		List<ITransaction> transactions;

		Clope clope;

		DataSetupSettings settings;
		ILogger logger;
		IAttributeStore attributeStore;

		// for logger and watch
		Stopwatch watch;
		Stopwatch stepWatch;



		// init
		public Tester(Clope clope, DataSetupSettings settings, ILogger logger)
		{
			this.clope = clope ?? throw new ArgumentNullException();
			this.logger = logger ?? new ConsoleLogger();
			this.settings = settings;
			watch = new Stopwatch();
			stepWatch = new Stopwatch();
			transactions = new List<ITransaction>();
			input = new List<string>();
		}



		// public Run.
		public void Run()
		{
			watch.Reset();
			stepWatch.Reset();
			logger.WriteDated("Tester start.");

			PrepareTest();
#if mult
			var multipleTimesRead = 7;// 170;
			while (multipleTimesRead-- > 0)
			{
				logger.Write($"times left: {multipleTimesRead}");
				ReadData();
			}
#else
			ReadData();
#endif
			RunClope();

			logger.WriteDated("Tester finished.\r\n");

			watch.Stop();
			stepWatch.Stop();
		}



		// factory
		const int LinesToReadAtOnceForExample = 423;
		IPortionReader _GetReader() => new Reader(settings.SelectedDelimitedFile.GetPath()) { LinesToReadAtOnce = LinesToReadAtOnceForExample };
		IParser _GetParser() => new Parser(settings.SelectedDelimitedFile.FieldSeparators, new ElementRule(determineAsNulls, null));
		IAttributeStore _GetAttributeStore() => new AttributeStoreAtDic();



		// logic
		void PrepareTest()
		{
			attributeStore = _GetAttributeStore();

			logger.Write($"Prepare test>");
			logger.Write($"> file      > {settings.SelectedDelimitedFile.GetPath()}");
			logger.Write($"> repulsion > {settings.ClopeRepulsion}");

			clope.Repulsion = settings.ClopeRepulsion;
			clope.StepDone += (step, changes) => StepInfo(step, changes);
			logger.Write("------\n");
		}



		void ReadData()
		{
			LoggingStart("Read");
			IPortionReader reader = _GetReader();
			IParser parser = _GetParser();

			reader.SkipLines(settings.SelectedDelimitedFile.FirstLinesToSkip);

			while (!reader.ReachedEndOfFile)
			{
				var tempTrans = new List<ITransaction>();

				// get transactions from data portion:
				foreach (var possibleTransaction in reader.GetData())
				{
					input.Add(possibleTransaction);
					var attributes = parser.Parse(possibleTransaction);
					var t = new Transaction(attributeStore.PlaceAndGetLinks(attributes));
					tempTrans.Add(t);
				}

				transactions.AddRange(tempTrans);

				clope.AddNewTransactions(tempTrans.ToArray());
			}

			LoggingEnd("Read");
		}



		void RunClope()
		{
			LoggingStart("Clope");
			stepWatch.Start();
			clope.Run();
			LoggingEnd("Clope");
		}



		public string MakeResults(int column = 0)
		{
			LoggingStart("Results");
			var preview = new Previewer(clope.GetTransactions_Axe(), clope.Clusters, attributeStore);
			preview.MakePreview(column);

			LoggingEnd("Results");
			logger.Write($"Steps done: {clope.LatestStep}.");

			return preview.GetOutput();
		}



		// for outputs:
		string ElapsedMs(Stopwatch w) => $"{w.ElapsedMilliseconds} ms";
		string Elapsed(Stopwatch w) => $"{w.Elapsed.ToString()}";

		void StepInfo(int step, int changesDone)
		{
			LoggingEnd($"On step {step} - {changesDone} changes were done.", stepWatch, false);
			stepWatch.Restart();
		}

		void LoggingStart(string name) => LoggingStart(name, watch);

		void LoggingStart(string name, Stopwatch watch)
		{
			//logger.Write($"{name}> start...");
			watch.Restart();
		}

		void LoggingEnd(string name) => LoggingEnd(name, watch);

		void LoggingEnd(string name, Stopwatch watch, bool bonusEndLine = true)
		{
			watch.Stop();
			logger.Write($"{name}> done. Time elapsed {Elapsed(watch)} ({ElapsedMs(watch)})");
			if (bonusEndLine)
				logger.Write("------");
		}
	}
}
