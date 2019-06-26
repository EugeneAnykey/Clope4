﻿namespace ClopeLib
{
	public interface IAttributeStore
	{
		int PlaceAttribute(IAttribute at);

		int[] PlaceAndGetIndices(string[] items);

		IAttribute[] GetAttributes(int index);

		IAttribute GetAttributeById(int id);
	}
}
