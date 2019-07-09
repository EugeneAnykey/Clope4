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


		/// <summary>
		/// Returns attributes for specified position
		/// </summary>
		/// <param name="position">attribute position</param>
		/// <returns>attributes that are from specified position</returns>
		IAttribute[] GetAttributes(int position);


		/// <summary>
		/// Returns links to attributes for specified position
		/// </summary>
		/// <param name="position">attribute position</param>
		/// <returns>attribute's links that are from specified position</returns>
		int[] GetAttributesLinks(int position);


		/// <summary>
		/// Returns attribute from specified link in store
		/// </summary>
		/// <param name="link">store's link</param>
		/// <returns>linked attribute</returns>
		IAttribute GetAttributeByLink(int link);
	}
}
