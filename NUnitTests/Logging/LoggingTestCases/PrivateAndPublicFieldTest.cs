namespace NunitTests.Logging.LoggingTestCases
{
    internal class PrivateAndPublicFieldTest
	{
		public int publicfield;
		public int publicProperty { get; set; }
		private int privateField;
		private int privateProperty { get; set; }
	}
}
