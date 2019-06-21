using TestingHelper;

namespace ClopeWin
{
	public struct DataSetupSettings
	{
		public DelimitedFile SelectedDelimitedFile { get; set; }

		public float ClopeRepulsion { get; set; }
		public int NullColumn { get; set; }
		public int NullJumps { get; set; }
	}
}
