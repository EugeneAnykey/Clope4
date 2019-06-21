﻿using System;

namespace ClopeLib.Parcers
{
	public class Parcer : IParcer
	{
		// const
		public static readonly char[] CommaSplitter = { ',' };
		public static readonly char[] CommaSpaceSplitter = { ',', ' ' };
		public static readonly char[] SemicolonSplitter = { ';' };
		public static readonly char[] SemicolonSpaceSplitter = { ';', ' ' };



		// field
		public char[] Splitter { get; set; }

		public IElementRule Rule { get; }

		StringSplitOptions splitOption = StringSplitOptions.RemoveEmptyEntries;
		public bool RemoveEmptyEntries
		{
			get => splitOption == StringSplitOptions.RemoveEmptyEntries;
			set => splitOption = value ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
		}



		// init
		public Parcer(char[] splitter, IElementRule rule)
		{
			Splitter = splitter;
			Rule = rule ?? new ElementAlwaysValidRule();
		}

		public Parcer() : this(CommaSplitter, null) { }



		// public
		public string[] Parce(string line)
		{
			var result = line.Split(Splitter, splitOption);

			InvalidDataCorrection(ref result);

			return result;
		}



		// private
		void InvalidDataCorrection(ref string[] lines)
		{
			for (int i = 0; i < lines.Length; i++)
				Rule.Validate(ref lines[i]);
		}
	}
}
