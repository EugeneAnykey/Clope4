namespace ClopeCon
{
	public struct FileParams
	{
		// fields
		public string Filename;
		public float Repulsion;
		public char Separator;
		public int ColumnToView;
		public int FirstLinesToSkip;



		public void DefaultTest()
		{
			Filename = @"..\..\data\agaricus-lepiota.csv";
			Repulsion = 2.1f;
			Separator = ';';
			FirstLinesToSkip = 5;
			ColumnToView = 2;
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

		static char ParseSeparator(string val)
		{
			return
				val == "\t" ? '\t' :
				string.IsNullOrEmpty(val) ? ';' :
				val[0];
		}

		void Parse(string[] args)
		{
			const string paramNameRepulsion = "repulsion";
			const string paramNameFile = "file";
			const string paramNameColumn = "col";
			const string paramNameSeparator = "separator";
			const string paramNameSkipLines = "skip";

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

				else if (s.Contains(paramNameSeparator))
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
