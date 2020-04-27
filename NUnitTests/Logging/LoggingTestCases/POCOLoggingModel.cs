using CsvLogging.Attributes;

namespace NunitTests.Logging.LoggingTestCases
{

	internal class POCOLoggingModel
	{
		[CsvName("PublicStringPropertyInLoggingModel")]
		public string PublicStringProperty { get; set; } = "";
		[CsvName("PublicIntegerPropertyInLoggingModel")]
		public int PublicIntegerProperty { get; set; }

		public string publicStringField = ""; 

		public int publicIntField;
	}
}
