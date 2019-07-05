using System.Diagnostics;

namespace ClopeLib.Data
{
	[DebuggerDisplay("Atr: {Position} - {Name}.")]
	public struct Attribute4 : IAttribute
	{
		// field
		public int Position { get; }

		public string Name { get; }



		// init
		public Attribute4(int position, string name)
		{
			Position = position;
			Name = name;
		}



		// public
		public bool Equals(IAttribute other) => Position == other.Position && Name == other.Name;
	}
}
