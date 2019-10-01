namespace ClopeLib.Data
{
	internal struct AttributeAlt<T>
	{
		public T Value { get; }

		public AttributeAlt(T val) => Value = val;

		public bool Equals(AttributeAlt<T> other)
		{
			if (Value == null && other.Value == null)
				return true;

			return Value.Equals(other.Value);
		}
	}
}
