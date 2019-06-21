using System.Diagnostics;

namespace TestingHelper.UnitTests
{
	[DebuggerDisplay("{Id}:{Line} #{Hash}")]
	public struct HasherTestItem
	{
		public int Id;
		public string Line;
		public int Hash;

		public HasherTestItem(int id, string line, int hash)
		{
			Id = id;
			Line = line;
			Hash = hash;
		}
	}
}
