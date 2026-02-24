using EXCSLA.Shared.Domain.Tests.BaseTestObjects;
using System;

namespace EXCSLA.Shared.Domain.Tests.Builders
{
	public class BaseEntityBuilder
	{
		public static int DEFAULT_ID = 1;
		public static string DEFAULT_FIRST_NAME = "Harold";
		public static string DEFAULT_LAST_NAME = "Collins";

		private TestIntBaseEntity _baseEntity;

		public BaseEntityBuilder(int id, string firstName, string lastName)
		{
			_baseEntity  = new TestIntBaseEntity(id, firstName, lastName);
		}

		public TestIntBaseEntity Build()
		{
			return _baseEntity;
		}

		public static TestIntBaseEntity GetDefaultTestBaseEntity()
		{
			var tvo = new BaseEntityBuilder(DEFAULT_ID, DEFAULT_FIRST_NAME, DEFAULT_LAST_NAME);
			return tvo.Build();
		}
	}

	public class GuidBaseEntityBuilder
	{
		public static Guid DEFAULT_ID = Guid.NewGuid();
		public static string DEFAULT_FIRST_NAME = "Harold";
		public static string DEFAULT_LAST_NAME = "Collins";

		private TestGuidBaseEntity _baseEntity;

		public GuidBaseEntityBuilder(Guid id, string firstName, string lastName)
		{
			_baseEntity = new TestGuidBaseEntity(id, firstName, lastName);
		}

		public TestGuidBaseEntity Build()
		{
			return _baseEntity;
		}

		public static TestGuidBaseEntity GetDefaultTestBaseEntity()
		{
			var tvo = new GuidBaseEntityBuilder(DEFAULT_ID, DEFAULT_FIRST_NAME, DEFAULT_LAST_NAME);
			return tvo.Build();
		}
	}

	public class StringBaseEntityBuilder
	{
		public static string DEFAULT_ID = "string-id-1";
		public static string DEFAULT_FIRST_NAME = "Harold";
		public static string DEFAULT_LAST_NAME = "Collins";

		private TestStringBaseEntity _baseEntity;

		public StringBaseEntityBuilder(string id, string firstName, string lastName)
		{
			_baseEntity = new TestStringBaseEntity(id, firstName, lastName);
		}

		public TestStringBaseEntity Build()
		{
			return _baseEntity;
		}

		public static TestStringBaseEntity GetDefaultTestBaseEntity()
		{
			var tvo = new StringBaseEntityBuilder(DEFAULT_ID, DEFAULT_FIRST_NAME, DEFAULT_LAST_NAME);
			return tvo.Build();
		}
	}

	public class LongBaseEntityBuilder
	{
		public static long DEFAULT_ID = 1000000000L;
		public static string DEFAULT_FIRST_NAME = "Harold";
		public static string DEFAULT_LAST_NAME = "Collins";

		private TestLongBaseEntity _baseEntity;

		public LongBaseEntityBuilder(long id, string firstName, string lastName)
		{
			_baseEntity = new TestLongBaseEntity(id, firstName, lastName);
		}

		public TestLongBaseEntity Build()
		{
			return _baseEntity;
		}
	}
}
