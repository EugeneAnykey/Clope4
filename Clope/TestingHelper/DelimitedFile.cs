using System;

namespace TestingHelper
{
	/// <summary>
	/// Delimited File.
	/// </summary>
	public class DelimitedFile
	{
		// default:
		readonly char[] fieldSeparatorDefault = new[] { ',' };
		const string recordSeparatorDefault = "\r\n";



		// field:
		public string Filename { get; }

		/// <summary>
		/// Elements of transactions separator
		/// </summary>
		public char[] FieldSeparators { get; }

		/// <summary>
		/// Line, transaction separator
		/// </summary>
		public string RecordSeparator { get; }

		/// <summary>
		/// Determines how much lines to skip at the file begining. Less than 1 means do no skip.
		/// </summary>
		public int FirstLinesToSkip { get; set; }

		/// <summary>
		/// If true than it combine same fieldSeparators into one.
		/// </summary>
		public bool CombineSameDelimeters { get; set; }



		/// <summary>
		/// File Broker that makes file path
		/// </summary>
		public IFileBroker Broker { get; set; }



		// init
		public DelimitedFile(string filename, char[] fieldSeparators, string recordSeparator = null)
		{
			Filename = filename ?? throw new ArgumentNullException();

			FirstLinesToSkip = 0;

			FieldSeparators =
				fieldSeparators == null || fieldSeparators.Length == 0 ?
				fieldSeparatorDefault :
				fieldSeparators;

			RecordSeparator =
				string.IsNullOrEmpty(recordSeparator) ?
				recordSeparatorDefault :
				recordSeparator;
		}



		public override string ToString() => Filename;

		public string GetPath() => Broker == null ? Filename : Broker.GetFilePath(this);
	}
}
