using System;
using System.Collections.Generic;
using System.Diagnostics;
using ClopeLib;
using ClopeLib.Algo;
using ClopeLib.Data;
using ClopeLib.Parcers;
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
		IAttributeStore attributeStore;
		//IClustering clope;
		Clope4 clope;
		DataSetupSettings settings;
		ILogger logger;
		IPortionReader reader;
		IParcer parcer;

		// for logger and watch
		Stopwatch watch;
		Stopwatch stepWatch;
		string ElapsedMs => $"{watch.ElapsedMilliseconds} ms";
		string Elapsed => $"{watch.Elapsed.ToString()}";



		void LoggerAndWatchStart(string name) => LoggerAndWatchStart(name, watch);

		void LoggerAndWatchStart(string name, Stopwatch watch)
		{
			logger.Write($"{name}> start...");
			watch.Restart();
		}



		void LoggerAndWatchEnd(string name) => LoggerAndWatchEnd(name, watch);
		
		void LoggerAndWatchEnd(string name, Stopwatch watch)
		{
			watch.Stop();
			logger.Write($"{name}> done. Time elapsed {Elapsed} ({ElapsedMs})\n------\n");
		}



		// init
		public Tester4(Clope4 clope, DataSetupSettings settings, ILogger logger)
		{
			this.clope = clope ?? throw new ArgumentNullException();
			this.logger = logger ?? new ConsoleLogger();
			this.settings = settings;
			watch = new Stopwatch();
			stepWatch = new Stopwatch();
			attributeStore = new AttributeStoreAtList();
		}



		// public
		public void Run()
		{
			watch.Reset();
			stepWatch.Reset();
			logger.WriteDated("Tester start.");

			PrepareTest();
			ReadData();
			RunTest();

			logger.WriteDated("Tester finished.\r\n");

			watch.Stop();
			stepWatch.Stop();
		}



		// factory
		IPortionReader _GetReader() => new Reader(settings.SelectedDelimitedFile.GetPath()) { LinesToReadAtOnce = 243 };
		IParcer _GetParcer() => new Parcer(settings.SelectedDelimitedFile.FieldSeparators, new ElementRule(determineAsNulls, null));



		// logic
		void PrepareTest()
		{
			logger.Write("Prepare test> new reader, parcer, etc.");
			logger.Write($"> file > {settings.SelectedDelimitedFile.GetPath()}");

			reader = _GetReader();
			parcer = _GetParcer();

			reader.GetData(settings.SelectedDelimitedFile.FirstLinesToSkip);
			clope.Repulsion = settings.ClopeRepulsion;
			clope.StepDone += (step, changes) => StepInfo(step, changes);
			logger.Write("------\n");
		}



		void ReadData()
		{
			LoggerAndWatchStart("Read");

			while (!reader.ReachedEndOfFile)
			{
				var tempTrans = new List<ITransaction>();

				// get transactions from data portion:
				foreach (var possibleTransaction in reader.GetData())
				{
					var attributes = parcer.Parce(possibleTransaction);
					tempTrans.Add(new Transaction4(attributeStore.PlaceAndGetLinks(attributes)));
				}

				clope.AddNewTransactions(tempTrans.ToArray());
			}

			LoggerAndWatchEnd("Read");
		}



		void StepInfo(int step, int changesDone)
		{
			LoggerAndWatchEnd($"On step {step} - {changesDone} changes were done.", stepWatch);
			stepWatch.Restart();
		}

		void RunTest()
		{
			LoggerAndWatchStart("Clope");
			stepWatch.Start();
			clope.Run();
			LoggerAndWatchEnd("Clope");
		}



		// reports
		public string MakeResults(int column = 0)
		{
			LoggerAndWatchStart("Results");

			var preview = new Previewer4(clope.Transactions, clope.Clusters, attributeStore);
			preview.MakePreview(column);

			LoggerAndWatchEnd("Results");
			logger.Write($"Steps done: {clope.LatestStep}.");

			return preview.GetOutput();
		}
	}
}
