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
			Filename = @"..\..\..\..\data\agaricus-lepiota.csv";
			Repulsion = 2.5f;
			Separator = ',';
			FirstLinesToSkip = 1;
			ColumnToView = 1;
		}



		public void ParseArgs(string[] args)
		{
			Parse(args);
		}



		// Parse
		static string SeparateParam(string input) => input.Substring(input.IndexOf("=") + 1);

		void Parse(string[] args)
		{
			const string paramNameRepulsion = "repulsion";
			const string paramNameFile = "file";
			const string paramNameColumn = "col";
			const string paramNameSeparator = "separator";
			const string paramNameSkipLines = "skip";

			//Console.WriteLine("Command line parameters:");

			foreach (var s in args)
			{
				//Console.WriteLine(s);

				if (s.Contains(paramNameRepulsion))
				{
					if (float.TryParse(SeparateParam(s), out float val))
						Repulsion = val;
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
					char sep = ';';
					var val = SeparateParam(s);
					if (val == "\t")
						sep = '\t';
					else if (string.IsNullOrEmpty(val))
						sep = ';';
					else sep = val[0];

					Separator = sep;
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
