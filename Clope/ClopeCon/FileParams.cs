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
	}
}
