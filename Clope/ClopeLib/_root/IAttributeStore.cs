namespace ClopeLib
{
	public interface IAttributeStore
	{
		int PlaceAttribute(IAttribute at);

		int[] GetIndices(string[] items);
	}
}
