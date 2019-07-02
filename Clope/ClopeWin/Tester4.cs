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
	public class Tester4
	{
		// predefs
		readonly string[] determineAsNulls = new[] { "?", string.Empty };



		// field
		List<string> input;
		List<ITransaction> transactions;
		IAttributeStore attributeStore;

		Clope4 clope;

		DataSetupSettings settings;
		ILogger logger;
		IPortionReader reader;
		IParser parser;

		// for logger and watch
		Stopwatch watch;
		Stopwatch stepWatch;



		// init
		public Tester4(Clope4 clope, DataSetupSettings settings, ILogger logger)
		{
			this.clope = clope ?? throw new ArgumentNullException();
			this.logger = logger ?? new ConsoleLogger();
			this.settings = settings;
			watch = new Stopwatch();
			stepWatch = new Stopwatch();
			attributeStore = new AttributeStoreAtList();
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
			ReadData();
			RunClope();

			logger.WriteDated("Tester finished.\r\n");

			watch.Stop();
			stepWatch.Stop();
		}



		// factory
		const int LinesToReadAtOnceForExample = 423;
		IPortionReader _GetReader() => new Reader(settings.SelectedDelimitedFile.GetPath()) { LinesToReadAtOnce = LinesToReadAtOnceForExample };
		IParser _GetParser() => new Parser(settings.SelectedDelimitedFile.FieldSeparators, new ElementRule(determineAsNulls, null));



		// logic
		void PrepareTest()
		{
			logger.Write("Prepare test> new reader, parser, etc.");
			logger.Write($"> file > {settings.SelectedDelimitedFile.GetPath()}");
			logger.Write($"> repulsion > {settings.ClopeRepulsion}");

			reader = _GetReader();
			parser = _GetParser();

			clope.Repulsion = settings.ClopeRepulsion;
			clope.StepDone += (step, changes) => StepInfo(step, changes);
			logger.Write("------\n");
		}



		void ReadData()
		{
			LoggingStart("Read");

			reader.SkipLines(settings.SelectedDelimitedFile.FirstLinesToSkip);

			while (!reader.ReachedEndOfFile)
			{
				var tempTrans = new List<ITransaction>();

				// get transactions from data portion:
				foreach (var possibleTransaction in reader.GetData())
				{
					input.Add(possibleTransaction);
					var attributes = parser.Parse(possibleTransaction);
					var t = new Transaction4(attributeStore.PlaceAndGetLinks(attributes));
					tempTrans.Add(t);
					//transactions.Add(t);
				}

				transactions.AddRange(tempTrans);

				clope.AddNewTransactions(tempTrans.ToArray());
			}

			//clope.AddNewTransactions(transactions.ToArray());

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

			var preview = new Previewer4(clope.GetTransactions_Axe(), clope.Clusters, attributeStore);
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
			logger.Write($"{name}> start...");
			watch.Restart();
		}

		void LoggingEnd(string name) => LoggingEnd(name, watch);

		void LoggingEnd(string name, Stopwatch watch, bool bonusEndLine = true)
		{
			watch.Stop();
			var el = bonusEndLine ? "------\n" : "";
			logger.Write($"{name}> done. Time elapsed {Elapsed(watch)} ({ElapsedMs(watch)})\n{el}");
		}
	}
}
