using System;

namespace ClopeCon
{
	public struct FileParams
	{
		const string paramNameRepulsion = "rep";
		const string paramNameFile = "file";
		const string paramNameColumn = "col";
		const string paramNameDelimeter = "delim";
		const string paramNameSkipLines = "skip";

		// fields
		public string Filename;
		public float Repulsion;
		public char Separator;
		public int ColumnToView;
		public int FirstLinesToSkip;



		public void DefaultTest()
		{
			Filename = @"";
			Repulsion = 2.0f;
			Separator = ';';
			FirstLinesToSkip = 0;
			ColumnToView = 0;
		}



		public void ParseArgs(string[] args)
		{
			Parse(args);
		}



		// Parse
		static string SeparateParam(string input) => input.Substring(input.IndexOf("=") + 1);

		static float TryParseFloat(string number, float defaultValue = 0)
		{
			var nfi = new System.Globalization.NumberFormatInfo
			{
				NumberDecimalSeparator = "."
			};

			return
				float.TryParse(
					number.Replace(",", "."),
					System.Globalization.NumberStyles.Float,
					nfi,
					out float val
				) ?
				val :
				defaultValue;
		}

		internal static string GetHelp()
		{
			return string.Join("\n",
				new string[] {
					"Proper use:",
					$"\t<app.exe> {paramNameFile}=<filename> {paramNameRepulsion}=<repulsion> {paramNameDelimeter}=<items separator> {paramNameColumn}=<view> {paramNameSkipLines}=<lines>",
					"",
					"where:",
					"\t<app.exe> - application name,",
					"\t<filename> - filename with structured data,",
					"\t<repulsion> - clope repulsion (should be between 1.0 to 5.0),",
					"\t<separator> - item separator, e.g. comma, semicolon or '\\t' for tab,",
					"\t<col> - column to be showed,",
					"\t<skip> - first lines to be skipped.",
					"",
					"E.g:",
					"\tClopeCon.exe rep=2.6 file=data.csv col=0 delim=, skip=1"
				}
			);
		}

		static char ParseSeparator(string val)
		{
			return
				val == "\t" ? '\t' :
				string.IsNullOrEmpty(val) ? ';' :
				val[0];
		}

		void Parse(string[] args)
		{
			foreach (var s in args)
			{
				if (s.Contains(paramNameRepulsion))
				{
					Repulsion = TryParseFloat(SeparateParam(s), 2);
				}

				else if (s.Contains(paramNameColumn))
				{
					if (int.TryParse(SeparateParam(s), out int val))
						ColumnToView = val;
				}

				else if (s.Contains(paramNameFile))
				{
					Filename = SeparateParam(s);
				}

				else if (s.Contains(paramNameDelimeter))
				{
					Separator = ParseSeparator(SeparateParam(s));
				}

				else if (s.Contains(paramNameSkipLines))
				{
					if (int.TryParse(SeparateParam(s), out int val))
						FirstLinesToSkip = val;
				}
			}
		}
	}
}
