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
	public class Tester
	{
		// predefs
		readonly string[] determineAsNulls = new[] { "?", string.Empty };



		// field
		Clope clope;
		DataSetupSettings settings;
		Nullabier nullabier;

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
		public Tester(Clope clope, DataSetupSettings settings, ILogger logger)
		{
			this.watch = new Stopwatch();
			this.clope = clope ?? throw new ArgumentNullException();
			this.logger = logger ?? new ConsoleLogger();
			this.settings = settings;
		}



		// public
		public void Run()
		{
			PrepareTest();
			ReadData();
			RunTest();
		}



		// factory
		Nullabier _GetNullabier() => new Nullabier(settings.NullColumn, settings.NullJumps, "?");
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
			nullabier = _GetNullabier();
			logger.Write(string.Format("> Nullabier {0} initiated.", nullabier.Initiated ? "" : "NOT"));
			if (!nullabier.Initiated)
				nullabier = null;

			reader.GetData(settings.SelectedDelimitedFile.FirstLinesToSkip);
			clope.Repulsion = settings.ClopeRepulsion;
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
					nullabier?.MaybePlaceNull(ref items);
					// TODO is here! new Transaction(line) !!!
					tempTrans.Add(new Transaction(items));
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

			var preview = new Previewer(clope.Transactions, clope.Clusters);
			preview.PrepareView(column);

			LoggerAndWatchEnd("Results");
			logger.WriteDated("End");

			return preview.Output();
		}
	}
}
