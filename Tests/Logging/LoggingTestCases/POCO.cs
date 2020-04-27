using DataModels.Logging.Attributes;

namespace NunitTests.Logging.LoggingTestCases
{
	[LoggingModel(typeof(POCOLoggingModel))]
	internal class POCO
	{
		public string PublicStringProperty { get; set; } = "";
		public int PublicIntegerProperty { get; set; }

		public string publicStringField = "";

		public int publicIntField;

		public double ShoulntGetLogged;

		public int PropertyThatShouldntBeLogged { get; set; }
	}
}
