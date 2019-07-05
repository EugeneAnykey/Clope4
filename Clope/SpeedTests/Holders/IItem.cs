using System;
using System.Diagnostics;

namespace SpeedTests.Holders
{
	public interface IItem : IEquatable<IItem>
	{
		int Id { get; set; }
		string Name { get; set; }
	}



	[DebuggerDisplay("ClassItem: {Id} - {Name}.")]
	public class ClassItem : IItem
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ClassItem(int index, string name)
		{
			Id = index;
			Name = name;
		}

		//public bool Equals(ClassItem other) => Id == other.Id && Name == other.Name;

		public bool Equals(IItem other) => other is ClassItem? Id == other.Id && Name == other.Name : false;
	}



	[DebuggerDisplay("StructItem: {Id} - {Name}.")]
	public struct StructItem : IItem
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public StructItem(int index, string name)
		{
			Id = index;
			Name = name;
		}

		//public bool Equals(ClassItem other) => Id == other.Id && Name == other.Name;

		public bool Equals(IItem other) => other is StructItem ? Id == other.Id && Name == other.Name : false;
	}
}
