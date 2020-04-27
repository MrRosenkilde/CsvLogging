using CsvLogging.Attributes;
using NUnitTests.TestData;

namespace NunitTests.Logging.LoggingTestCases
{
	internal class TestDontLogAttribute
	{
		[DontLog]
		public int ThisPropertyIsNotLogged { get; set; }
		[DontLog]
		public int ThisFieldIsNotLogged;
		public int ThisFieldIsLogged { get; set; }
		public int ThisPropertyIsLogged { get; set; }

		[DontLog]
		public ReadonlyPoint3D ThisStructIsNotLogged { get; set; }
	}
}
