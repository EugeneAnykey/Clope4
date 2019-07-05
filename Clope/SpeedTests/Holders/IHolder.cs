namespace SpeedTests.Holders
{
	public interface IHolder
	{
		int[] PlaceAndGetIndicies(params string[] items);

		IItem[] Retrieve(int id);

		IItem RetrieveByIndex(int index);
	}
}
