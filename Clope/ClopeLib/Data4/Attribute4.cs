using System.Diagnostics;

namespace ClopeLib.Data
{
	[DebuggerDisplay("Attribute #{Link} = {Position}:{Name}.")]
	public class Attribute4 : IAttribute
	{
		public int Link { get; set; }

		public int Position { get; }

		public string Name { get; }



		// init
		public Attribute4(int position, string name)
		{
			Link = 0;
			Position = position;
			Name = name;
		}

		public Attribute4(int position, string name, int link)
		{
			Link = link;
			Position = position;
			Name = name;
		}

		public bool Equals(IAttribute other) => Similar(other);

		bool Similar(IAttribute other) => Position == other.Position && Name == other.Name;
	}
}
