using System;

namespace ClopeWin
{
	public class Nullabier
	{
		// const
		const int MinNullVal = 10;
		const int MinNullRndVal = 5;



		// field
		readonly static Random r = new Random(DateTime.UtcNow.Millisecond);

		public bool Initiated { get; private set; }

		readonly int nullColumn;
		readonly int nullStep = 0;
		readonly int nullRandomVal = 0;
		readonly int offset = 0;
		readonly string nullSymbol;

		int currentLine = 0;
		int nextStop = 0;



		// init
		public Nullabier(int nullColumn, int nullJumps, string nullSymbol)
		{
			Initiated = nullColumn > 0 && nullJumps > 0;

			if (!Initiated)
				return;

			this.nullColumn = nullColumn - 1;
			nullStep = nullJumps > MinNullVal ? nullJumps : MinNullVal;

			offset = nullStep / 3;
			nullRandomVal = nullStep / 10;
			nullRandomVal = nullRandomVal > MinNullRndVal ? nullRandomVal : MinNullRndVal;

			this.nullSymbol = nullSymbol;

			MakeNextStop();
		}



		// public
		public void MaybePlaceNull(ref string[] items)
		{
			if (currentLine == nextStop)
			{
				if (items.Length > nullColumn)
				{
					items[nullColumn] = nullSymbol;
					MakeNextStop();
				}
				else
				{
					nextStop++;
				}
			}

			currentLine++;
		}



		// private
		void MakeNextStop() => nextStop = nullStep + r.Next(nullRandomVal) - offset;
	}
}
