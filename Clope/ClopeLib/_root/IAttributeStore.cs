namespace ClopeLib
{
	public interface IAttributeStore
	{
		/// <summary>
		/// Exchanging attributes for links. Storing items for reverse exchange.
		/// </summary>
		/// <param name="items">attributes (position in array and item)</param>
		/// <returns>links of stored attributes</returns>
		int[] PlaceAndGetLinks(params string[] items);

		IAttribute[] GetAttributes(int position);

		int[] GetAttributesLinks(int position);

		IAttribute GetAttributeByLink(int link);
	}
}
