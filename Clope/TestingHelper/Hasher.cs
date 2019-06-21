namespace TestingHelper
{
	public static class Hasher
	{
		/* [numbers]:
			int seed = 0x17;
			0xad = 173;
			0x7f = 127 = 2^7 - 1;
			0x1f =  31 = 2^5 - 1;
			0x17 =  23;
			0x07 =   7 = 2^3 - 1;
		 */

		/* [example]:
			unchecked
			{
				hashBase = (hashBase* 7) + x;
				hashBase = (hashBase* 7) + y;
			}
		*/



		// main:
		public static int GetHashCode(int id, string s)
		{
			const int seed = 0xad;

			int hash = seed;
			unchecked
			{
				hash += id;
				var sh = s != null ? s.GetHashCode() : -1;
				hash = (hash * seed) + sh;
			}

			return hash;
		}



		#region for test purpose
		public static int GetHashCode_Test1(int id, string s)
		{
			int hashBase = 31;

			var res = id != 0 ? id : 31127;
			var shash = s.GetHashCode();

			if (s != null)
				unchecked { res *= hashBase * shash; }

			return res;
		}



		public static int GetHashCode_Test2(int id, string s)
		{
			const int seed = 0xad;

			int hash = seed;
			unchecked
			{
				hash += id;
				hash += seed * s.GetHashCode();
			}

			return hash;
		}
		#endregion
	}
}
