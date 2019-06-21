using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingHelper.UnitTests
{
	public class HasherDataPreparer
	{
		// const
		static readonly char[] splitterLine = new char[] { '\r', '\n' };
		static readonly char[] splitterTab = new char[] { '\t' };
		static readonly char[] splitterComma = new char[] { ',' };
		static readonly string ConsiderAsNull = "?";



		#region Get lines for tests
		static string[] GetLines_LatinAlphabet => "abcdefghijklmnopqrstuvwxyz".ToCharArray().Select(c => c.ToString()).ToArray();

		static string[] GetLines_IdWithLetters =>
			@"name	lines
			1	p, e
			2	x, b, s, f, k, c
			3	s, y, f, g
			4	n, y, w, g, e, p, b, u, c, r
			5	t, f
			6	p, a, l, n, f, c, y, s, m
			7	f, a
			8	c, w
			9	n, b
			10	k, n, g, p, w, h, u, e, b, r, y, o
			11	e, t
			12	e, c, b, r, ?
			13	s, f, k, y
			14	s, f, y, k
			15	w, g, p, n, b, e, o, c, y
			16	w, p, g, b, n, e, y, o, c
			17	p
			18	w, n, o, y
			19	o, t, n
			20	p, e, l, f, n
			21	k, n, u, h, w, r, o, y, b
			22	s, n, a, v, y, c
			23	u, g, m, d, p, w, l"
		.Split(splitterLine, StringSplitOptions.RemoveEmptyEntries);

		static string[] GetLines_IdWithWords =>
			@"name	lines
			1	ядовитый, съедобный
			2	выпуклая, колокол, вогнутая, плоская, с бугорком, коническая
			3	гладкая, чешуйчатая, волокнистая, трещиноватая
			4	коричневый, желтый, белый, серый, красный, розовый, бежевый, фиолетовый, корица, зеленый
			5	есть, нет
			6	острый, миндаля, аниса, отсутствует, грязи, пряный, рыбный, затхлый
			7	свободный, приросший
			8	закрытые, частые
			10	черный, коричневый, серый, розовый, белый, шоколад, фиолетовый, красный, бежевый, зеленый, желтый, оранжевый
			11	расширенная к основанию, сужающаяся к основанию
			12	цилиндрический, клубневидный, луковичный, ризоморфа, отсутствует
			13	гладкая, тонковолокнистая, шелковистая, чешуйчатая
			14	гладкая, тонковолокнистая, чешуйчатая, шелковистая
			15	белый, серый, розовый, коричневый, бежевый, красный, оранжевый, корица, желтый
			16	белый, розовый, серый, бежевый, коричневый, красный, желтый, оранжевый, корица
			17	частное
			18	белый, коричневый, оранжевый, желтый
			19	одно, два, отсутствуют
			20	подвесная, затухающая, большая, сжигающаяся, отсутствуют
			21	черный, коричневый, фиолетовый, шоколад, белый, зеленый, оранжевый, желтый, бежевый
			22	разбросанные, многочисленные, обильные, редкие, одиночные, семействами
			23	город, трава, луга, леса, дороги, отходы, листва"
		.Split(splitterLine, StringSplitOptions.RemoveEmptyEntries);

		static string[] GetLines_FourEqualPairs =>
			@"name	items
			1	a, b, c
			1	a, b
			1	b, c"
		.Split(splitterLine, StringSplitOptions.RemoveEmptyEntries);

		static string[] GetLines_ContainsEmpties =>
			@"name	items
			1	?, b, c
			2	a, ?
			3	?, c"
		.Split(splitterLine, StringSplitOptions.RemoveEmptyEntries);
		#endregion



		#region Lines with alphabet
		public static HasherTestItem[] GetTableWith_LatinAlphabet(bool doubled, byte ids)
		{
			return PrepareTable(
				doubled ? GetDoubledAlphabet(GetLines_LatinAlphabet) : GetLines_LatinAlphabet,
				ids
			);
		}

		static string[] GetDoubledAlphabet(string[] alphabet)
		{
			var list = new List<string>();
			foreach (var a1 in alphabet)
			{
				foreach (var a2 in alphabet)
				{
					list.Add($"{a1}{a2}");
				}
			}
			return list.ToArray();
		}

		static HasherTestItem[] PrepareTable(string[] lines, int count = 10)
		{
			const byte countMin = 10;
			const byte countMax = 50;
			count = count > countMin ? count > countMax ? countMax : count : countMin;

			var res = new List<HasherTestItem>();

			foreach (string s in lines)
			{
				for (int j = 0; j < count; j++)
				{
					res.Add(
						new HasherTestItem(j, s, Hasher.GetHashCode(j, s))
					);
				}
			}

			return res.ToArray();
		}
		#endregion



		#region Lines with id
		public static HasherTestItem[] GetTableWith_ContainsEmpties() => Split(GetLines_ContainsEmpties);

		public static HasherTestItem[] GetTableWith_FourEqualPairs() => Split(GetLines_FourEqualPairs);

		public static HasherTestItem[] GetTableWith_Letters() => Split(GetLines_IdWithLetters);

		public static HasherTestItem[] GetTableWith_Words() => Split(GetLines_IdWithWords);

		static HasherTestItem[] Split(string[] lines)
		{
			var res = new List<HasherTestItem>();

			foreach (var line in lines)
			{
				var row = line.Trim().Split(splitterTab, StringSplitOptions.RemoveEmptyEntries);
				if (!int.TryParse(row[0], out int id))
					continue;

				foreach (var item in row[1].Split(splitterComma, StringSplitOptions.RemoveEmptyEntries))
				{
					var outItem = item.Trim();
					outItem = outItem != ConsiderAsNull ? outItem : null;

					res.Add(new HasherTestItem(id, outItem, Hasher.GetHashCode(id, outItem)));
				}
			}

			return res.ToArray();
		}
		#endregion
	}
}
