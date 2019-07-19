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
		Clope clope;

		List<ITransaction> transactions;
		IAttributeStore attributeStore;

		DataSetupSettings settings;
		ILogger logger;

		Stopwatch mainWatch;
		Stopwatch stepWatch;



		// init
		public Tester(DataSetupSettings settings, ILogger logger)
		{
			this.logger = logger ?? new ConsoleLogger();
			this.settings = settings;

			mainWatch = new Stopwatch();
			stepWatch = new Stopwatch();
			transactions = new List<ITransaction>();
			this.clope = new Clope(transactions);

			logger.Write();
		}



		// public Run.
		public void Run()
		{
			mainWatch.Reset();
			stepWatch.Reset();
			logger.WriteDated("Tester start.");

			PrepareTest();
			ReadData();
			RunClope();

			logger.WriteDated("Tester finished.\r\n");

			mainWatch.Stop();
			stepWatch.Stop();
		}



		// factory
		const int LinesToReadAtOnceForExample = 1024;

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
			logger.Write("------");
		}



		void ReadData()
		{
			LoggingStart(mainWatch);
			IPortionReader reader = _GetReader();
			IParser parser = _GetParser();

			reader.SkipLines(settings.SelectedDelimitedFile.FirstLinesToSkip);

			while (!reader.ReachedEndOfFile)
			{
				var data = reader.GetData();
				var tempTrans = new List<ITransaction>(data.Count);

				// get transactions from data portion:
				foreach (var possibleTransaction in data)
				{
					var attributes = parser.Parse(possibleTransaction);
					var t = new Transaction(attributeStore.PlaceAndGetLinks(attributes));
					tempTrans.Add(t);
				}

				transactions.AddRange(tempTrans);

				clope.AddNewTransactions();
			}

			LoggingEnd("Read", mainWatch);
		}



		void RunClope()
		{
			LoggingStart(mainWatch);
			stepWatch.Start();
			clope.Run();
			LoggingEnd("Clope", mainWatch);
		}



		public string MakeResults(int column = 0)
		{
			LoggingStart(mainWatch);
			var preview = new Previewer(transactions, clope.Clusters, attributeStore);
			preview.MakePreview(column);

			LoggingEnd("Results", mainWatch);
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

		void LoggingStart(Stopwatch watch) => watch.Restart();

		void LoggingEnd(string name, Stopwatch watch, bool bonusEndLine = true)
		{
			watch.Stop();
			logger.Write($"{name}> done. Time elapsed {Elapsed(watch)} ({ElapsedMs(watch)})");
			if (bonusEndLine)
				logger.Write("------");
		}
	}
}
