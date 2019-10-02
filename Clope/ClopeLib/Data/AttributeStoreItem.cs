namespace ClopeLib.Data
{
	internal struct AttributeStoreItem<T>
	{
		public T Value { get; }

		public AttributeStoreItem(T val) => Value = val;

		public bool Equals(AttributeStoreItem<T> other)
		{
			if (Value == null && other.Value == null)
				return true;

			return Value.Equals(other.Value);
		}
	}
}
