namespace ClopeLib
{
	public interface IAttributeStore
	{
		int[] PlaceAndGetLinks(params string[] items);

		IAttribute[] GetAttributes(int position);

		IAttribute GetAttributeByLink(int link);
	}
}
