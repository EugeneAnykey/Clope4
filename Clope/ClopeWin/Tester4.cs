using System;
using System.Collections.Generic;
using System.Diagnostics;
using ClopeLib;
using ClopeLib.Algo4;
using ClopeLib.Data4;
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
		string ElapsedMs => $"{watch.ElapsedMilliseconds} ms";
		string Elapsed => $"{watch.Elapsed.ToString()}";



		void LoggerAndWatchStart(string name)
		{
			logger.Write($"{name}> start...");
			watch.Restart();
		}



		void LoggerAndWatchEnd(string name)
		{
			watch.Stop();
			logger.Write($"{name}> done. Time elapsed {Elapsed} ({ElapsedMs})\n------\n");
			//logger?.LinkedLogger?.Write("");
		}



		// init
		public Tester4(Clope4 clope, DataSetupSettings settings, ILogger logger)
		{
			this.clope = clope ?? throw new ArgumentNullException();
			this.logger = logger ?? new ConsoleLogger();
			this.settings = settings;
			watch = new Stopwatch();
			attributeStore = new AttributeStoreAtList();
		}



		// public
		public void Run()
		{
			PrepareTest();
			ReadData();
			RunTest();
		}



		// factory
		IPortionReader _GetReader() => new Reader(settings.SelectedDelimitedFile.GetPath()) { LinesToReadAtOnce = 243 };
		IParcer _GetParcer() => new Parcer(settings.SelectedDelimitedFile.FieldSeparators, new ElementRule(determineAsNulls, null));



		// logic
		void PrepareTest()
		{
			logger.WriteDated("Start.");
			logger.Write("Prepare test> new reader, parcer, etc.");
			logger.Write($"> file > {settings.SelectedDelimitedFile.GetPath()}");

			reader = _GetReader();
			parcer = _GetParcer();

			reader.GetData(settings.SelectedDelimitedFile.FirstLinesToSkip);
			clope.Repulsion = settings.ClopeRepulsion;
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
					var items = parcer.Parce(possibleTransaction);
					tempTrans.Add(new Transaction4(attributeStore.PlaceAndGetIndices(items)));
				}

				clope.AddNewTransactions(tempTrans.ToArray());
			}

			LoggerAndWatchEnd("Read");
		}



		void RunTest()
		{
			LoggerAndWatchStart("Clope");
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
			logger.Write($"Steps done: {clope.LatestStepIndex}.");
			logger.WriteDated("End");

			return preview.GetOutput();
		}
	}
}
