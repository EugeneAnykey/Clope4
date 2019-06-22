namespace ClopeLib
{
	public interface IAttributeGen<T> where T : class
	{
		int AttributeId { get; }
		int Attribute { get; }
		T AttributeName { get; }
	}
}
