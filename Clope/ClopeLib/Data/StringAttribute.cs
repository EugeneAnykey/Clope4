using System.Diagnostics;

namespace ClopeLib.Data
{
	[DebuggerDisplay("StringAttribute: {Position} - {Name}.")]
	public struct StringAttribute : IAttribute
	{
		// field
		public int Position { get; }

		public string Name { get; }



		// init
		public StringAttribute(int position, string name)
		{
			Position = position;
			Name = name;
		}



		// public
		public bool Equals(IAttribute other) => Position == other.Position && Name == other.Name;
	}
}
