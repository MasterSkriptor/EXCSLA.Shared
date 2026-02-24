using EXCSLA.Shared.Domain.Tests.BaseTestObjects;

namespace EXCSLA.Shared.Domain.Tests.Builders
{
	public class ValueObjectBaseBuilder
	{
		public static string DEFAULT_FIRST_NAME = "Harold";
		public static string DEFAULT_LAST_NAME = "Collins";

		private TestValueObject _valueObject;

		public ValueObjectBaseBuilder(string firstName, string lastName)
		{
			_valueObject = new TestValueObject(firstName, lastName);
		}

		public TestValueObject Build()
		{
			return _valueObject;
		}

		public static TestValueObject GetDefaultTestValueObject()
		{
			var tvo = new ValueObjectBaseBuilder(DEFAULT_FIRST_NAME, DEFAULT_LAST_NAME);
			return tvo.Build();
		}
	}
}
